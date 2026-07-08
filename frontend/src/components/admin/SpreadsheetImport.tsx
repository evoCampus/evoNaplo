import { useState, useRef, useEffect } from "react";
import { AlertCircle, Loader2, Download, CheckCircle2Icon, X } from "lucide-react";
import {
    Button, Card, Alert, AlertDescription, AlertTitle,
    Carousel, CarouselContent, CarouselItem, CarouselNext, CarouselPrevious,
    Dialog, DialogContent
} from "@evonaplo/ui-library";
import type { CreateStudentDTO } from "../../api";
import { useApiClient } from "../../hooks/use-api-client";

type ColumnConfig = {
    id: string;
    label: string;
    backendKey: string;
    type: "text" | "textarea";
    colSpan?: number;
};

type EditableStudentRow = {
    [key: string]: string | number | boolean;
    id: number;
    name: string;
    email: string;
    phone: string;
    program: string;
    firstTime: string;
    stayInTeam: string;
    goals: string;
    ts: string;
    isError: boolean;
};

type ApiError = {
    response?: {
        data?: unknown;
    };
    message?: string;
};

const STUDENT_COLUMNS: ColumnConfig[] = [
    { id: "ts", label: "Timestamp", backendKey: "timestamp", type: "text" },
    { id: "email", label: "Email Address", backendKey: "email", type: "text" },
    { id: "phone", label: "Phone Number", backendKey: "phoneNumber", type: "text" },
    { id: "program", label: "University / Program", backendKey: "major", type: "text" },
    { id: "firstTime", label: "First time participant?", backendKey: "isFirstTime", type: "text" },
    { id: "stayInTeam", label: "Staying in team?", backendKey: "stayInTeam", type: "text" },
    { id: "goals", label: "Goals", backendKey: "goals", type: "textarea", colSpan: 2 }
];

export default function SpreadsheetImport() {
    const apiClient = useApiClient();
    const [status, setStatus] = useState<"idle" | "loading" | "valid" | "invalid" | "done" | "aborted">("idle");
    const [action, setAction] = useState<"import" | "save">("import");
    const [rows, setRows] = useState<EditableStudentRow[]>([]);
    const fileInputRef = useRef<HTMLInputElement>(null);

    const lastSoundRef = useRef<{ type: string, time: number }>({ type: "", time: 0 });

    const playSound = (type: "error" | "success" | "loading") => {
        const now = Date.now();
        if (lastSoundRef.current.type === type && now - lastSoundRef.current.time < 500) return;

        lastSoundRef.current = { type, time: now };
        const audio = new Audio(`/sounds/${type}.mp3`);
        audio.play().catch(error => console.warn("Sound blocked by browser: ", error));
    };

    useEffect(() => {
        if (status === "invalid") playSound("error");
        if (status === "done") playSound("success");
        if (status === "loading" && action === "import") playSound("loading");

        if (status === "invalid" || status === "done" || status === "aborted") {
            const timer = setTimeout(() => setStatus("idle"), 3500);
            return () => clearTimeout(timer);
        }
    }, [status, action]);

    const handleFileUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (!file) return;

        e.target.value = "";
        setAction("import");
        setStatus("loading");

        if (!file.name.endsWith(".xlsx") && !file.name.endsWith(".csv")) {
            setStatus("invalid");
            return;
        }

        try {
            await new Promise((resolve) => setTimeout(resolve, 1200));
            await apiClient.data.dataImportPost(file);

            // Generated API currently types this endpoint as void, so there is no preview payload.
            // We mark import complete and keep the manual save carousel closed.
            setRows([]);
            setStatus("done");
        } catch (error) {
            console.error("Import error:", error);
            setStatus("invalid");
            setRows([]);
        }
    };

    const handleFieldChange = (id: number, field: string, value: string) => {
        setRows((prevRows) =>
            prevRows.map((row) =>
                row.id === id ? { ...row, [field]: value, isError: false } : row
            )
        );
    };

    const saveStudentToDb = async (row: EditableStudentRow) => {
        const semesterMatch = String(row.program).match(/(\d+)/);
        const currentSemester = semesterMatch ? parseInt(semesterMatch[1], 10) : 1;

        const programParts = String(row.program).split(',');
        const universityProgramme = programParts.length > 1 ? programParts[1].trim() : String(row.program);

        const isFirstTime = String(row.firstTime || "").toLowerCase().includes("igen");
        const stayInTeam = String(row.stayInTeam || "").toLowerCase().includes("igen");

        const studentPayload: CreateStudentDTO = {
            id: crypto.randomUUID(),
            name: row.name,
            email: row.email,
            phoneNumber: row.phone,
            universityName: null,
            universityProgramme: universityProgramme,
            currentSemester: currentSemester,
            isInTheirFirstSemester: isFirstTime,
            personalGoals: row.goals,
            hasAppliedForScholarship: false,
            hasScholarship: false,
            scholarshipDuration: new Date().toISOString(),
            hasAppliedForInternship: false,
            hasInternship: false,
            isWorkingStudent: false,
            workExperienceInSemesters: "0",
            wantsToStayWithCurrentTeam: stayInTeam,
            teamId: null,
        };

        await apiClient.students.apiStudentsPost(studentPayload);
    };

    const handleSave = async (idToSave: number) => {
        const row = rows.find(r => r.id === idToSave);
        if (!row) return;
        if (status === "loading") return;

        try {
            await saveStudentToDb(row);
            const newRows = rows.filter((r) => r.id !== idToSave);
            setRows(newRows);

            if (newRows.length === 0) {
                setStatus("done");
            }
        } catch (error: unknown) {
            const err = error as ApiError;
            console.error("Error saving student (Save):", err.response?.data || err.message || err);
            setAction("save");
            setStatus("invalid");
            setRows((prevRows) => prevRows.map((r) => r.id === idToSave ? { ...r, isError: true } : r));
        }
    };

    const handleSaveAll = async () => {
        if (status === "loading") return;

        setAction("save");
        setStatus("loading");

        const successIds: number[] = [];
        const errorIds: number[] = [];

        for (const row of rows) {
            try {
                await saveStudentToDb(row);
                successIds.push(row.id);
            } catch (error: unknown) {
                const err = error as ApiError;
                console.error(`Error saving ${row.name} (Save All):`, err.response?.data || err.message || err);
                errorIds.push(row.id);
            }
        }

        const remainingRows = rows.filter((r) => !successIds.includes(r.id)).map(r =>
            errorIds.includes(r.id) ? { ...r, isError: true } : r
        );

        setRows(remainingRows);

        if (remainingRows.length === 0) {
            setStatus("done");
        } else {
            setStatus("invalid");
        }
    };

    const handleAbort = () => {
        setRows([]);
        setStatus("aborted");
    };

    const isDialogOpen = rows.length > 0;
    const inputClasses = "w-full bg-transparent border border-transparent hover:border-border focus:border-ring focus:bg-background px-2 py-1 rounded-md outline-none transition-colors";

    return (
        <>
            <input type="file" accept=".csv, .xlsx" className="hidden" ref={fileInputRef} onChange={handleFileUpload} />

            <div className="fixed top-6 left-1/2 -translate-x-1/2 z-[100] w-full max-w-md pointer-events-none flex flex-col gap-2 transition-all duration-300">
                <div className={`absolute w-full transition-all duration-500 ease-in-out ${status === "loading" ? "translate-y-0 opacity-100 visible" : "-translate-y-24 opacity-0 invisible"}`}>
                    <Alert className="bg-background/95 backdrop-blur shadow-xl">
                        <Loader2 className="animate-spin h-4 w-4" />
                        <AlertTitle>{action === "import" ? "Importing..." : "Saving..."}</AlertTitle>
                        <AlertDescription>{action === "import" ? "Processing data from spreadsheet." : "Saving students to the database."}</AlertDescription>
                    </Alert>
                </div>
                <div className={`absolute w-full transition-all duration-500 ease-in-out ${status === "invalid" ? "translate-y-0 opacity-100 visible" : "-translate-y-24 opacity-0 invisible"}`}>
                    <Alert variant="destructive" className="bg-background/95 backdrop-blur shadow-xl">
                        <AlertCircle className="h-4 w-4" />
                        <AlertTitle>Error occurred</AlertTitle>
                        <AlertDescription>{action === "import" ? "Invalid file format or empty spreadsheet." : "Some records could not be saved (e.g., email already exists)."}</AlertDescription>
                    </Alert>
                </div>
                <div className={`absolute w-full transition-all duration-500 ease-in-out ${status === "done" ? "translate-y-0 opacity-100 visible" : "-translate-y-24 opacity-0 invisible"}`}>
                    <Alert className="border-green-500 text-green-500 bg-background/95 backdrop-blur shadow-xl">
                        <CheckCircle2Icon className="h-4 w-4" color="currentColor" />
                        <AlertTitle>Success</AlertTitle>
                        <AlertDescription>{action === "import" ? "Spreadsheet processed successfully." : "All records successfully saved to the database!"}</AlertDescription>
                    </Alert>
                </div>
                <div className={`absolute w-full transition-all duration-500 ease-in-out ${status === "aborted" ? "translate-y-0 opacity-100 visible" : "-translate-y-24 opacity-0 invisible"}`}>
                    <Alert className="bg-background/95 backdrop-blur shadow-xl border-border">
                        <X className="h-4 w-4" />
                        <AlertTitle>Aborted</AlertTitle>
                        <AlertDescription>The import process was cancelled.</AlertDescription>
                    </Alert>
                </div>
            </div>

            <Button variant="outline" onClick={() => fileInputRef.current?.click()} className="h-12 text-base font-medium border-border/50 hover:bg-muted/80 hover:text-foreground cursor-pointer rounded-xl flex items-center justify-center gap-2">
                <Download className="h-5 w-5" /> Import from XLSX/CSV
            </Button>

            <Dialog open={isDialogOpen} onOpenChange={(open: boolean) => {
                if (!open) { setStatus("idle"); setRows([]); }
            }}>
                <DialogContent
                    style={{ border: "none", boxShadow: "none", background: "transparent" }}
                    className="max-w-[1000px] w-full flex justify-center bg-transparent border-none shadow-none p-0 outline-none ring-0 focus:outline-none focus:ring-0 focus-visible:outline-none focus-visible:ring-0 [&>button]:hidden"
                    onInteractOutside={(e: Event) => e.preventDefault()}
                    onOpenAutoFocus={(e: Event) => e.preventDefault()}
                >
                    <Carousel key={rows.length} className="w-[850px] min-w-[850px] max-w-[850px] relative outline-none focus:outline-none">
                        <CarouselContent className="ml-0 outline-none focus:outline-none">
                            {rows.map((r) => (
                                <CarouselItem key={r.id} className="pl-0 basis-full w-[850px] min-w-[850px] max-w-[850px] flex justify-center pb-4 pt-4 outline-none focus:outline-none">
                                    <Card className={`relative w-[750px] min-w-[750px] max-w-[750px] h-[550px] shrink-0 bg-background shadow-2xl rounded-2xl p-8 flex flex-col gap-6 outline-none focus:outline-none transition-colors duration-300 ${r.isError ? "border-2 border-destructive" : "border border-border/50"}`}>

                                        <button
                                            onClick={handleAbort}
                                            className="absolute top-6 right-6 text-muted-foreground hover:text-foreground hover:bg-muted/50 p-1.5 rounded-full transition-colors cursor-pointer"
                                            title="Cancel import"
                                        >
                                            <X className="h-5 w-5" />
                                        </button>

                                        <div className="border-b border-border/40 pb-4 pr-8">
                                            <input
                                                className="text-3xl font-bold tracking-tight w-full bg-transparent border border-transparent hover:border-border focus:border-ring focus:bg-background px-2 py-1 rounded-md outline-none transition-colors"
                                                value={r.name}
                                                onChange={(e) => handleFieldChange(r.id, "name", e.target.value)}
                                            />
                                        </div>

                                        <div className="grid grid-cols-2 gap-y-4 gap-x-8 text-sm max-h-[350px] overflow-y-auto pr-2 custom-scrollbar">
                                            {STUDENT_COLUMNS.map((col) => (
                                                <div
                                                    key={col.id}
                                                    className={`space-y-1 ${col.colSpan === 2 ? "col-span-2 mt-2 p-3 bg-muted/30 rounded-lg" : ""}`}
                                                >
                                                    <label className="text-muted-foreground font-semibold text-xs uppercase tracking-wider block px-2">
                                                        {col.label}
                                                    </label>

                                                    {col.type === "textarea" ? (
                                                        <textarea
                                                            className="font-medium text-foreground italic w-full min-h-[60px] max-h-[100px] overflow-y-auto resize-none bg-transparent border border-transparent hover:border-border focus:border-ring focus:bg-background px-2 py-1 rounded-md outline-none transition-colors custom-scrollbar"
                                                            value={String(r[col.id] ?? "")}
                                                            onChange={(e) => handleFieldChange(r.id, col.id, e.target.value)}
                                                        />
                                                    ) : (
                                                        <textarea
                                                            rows={1}
                                                            className={`font-medium text-foreground overflow-x-auto overflow-y-hidden whitespace-nowrap resize-none custom-scrollbar ${inputClasses}`}
                                                            value={String(r[col.id] ?? "")}
                                                            onChange={(e) => handleFieldChange(r.id, col.id, e.target.value)}
                                                        />
                                                    )}
                                                </div>
                                            ))}
                                        </div>

                                        <div className="flex justify-between gap-4 mt-auto pt-4 border-t border-border/40">
                                            <Button
                                                variant="secondary"
                                                className="w-36 cursor-pointer shadow-md"
                                                onClick={handleSaveAll}
                                                disabled={status === "loading"}
                                            >
                                                Save all
                                            </Button>
                                            <Button
                                                className="bg-green-600 hover:bg-green-700 w-36 rounded-full font-bold cursor-pointer text-white shadow-md disabled:bg-green-600/50"
                                                onClick={() => handleSave(r.id)}
                                                disabled={status === "loading"}
                                            >
                                                Save
                                            </Button>
                                        </div>

                                    </Card>
                                </CarouselItem>
                            ))}
                        </CarouselContent>
                        <CarouselPrevious className="absolute left-0 top-1/2 -translate-y-1/2 bg-background/60 hover:bg-background border-none text-foreground scale-125 backdrop-blur-md" />
                        <CarouselNext className="absolute right-0 top-1/2 -translate-y-1/2 bg-background/60 hover:bg-background border-none text-foreground scale-125 backdrop-blur-md" />
                    </Carousel>
                </DialogContent>
            </Dialog>
        </>
    );
}