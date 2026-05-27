import { useState, useRef, useEffect } from "react";
import { AlertCircle, Loader2, Download, CheckCircle2Icon } from "lucide-react";
import {
    Button, Card, Alert, AlertDescription, AlertTitle,
    Carousel, CarouselContent, CarouselItem, CarouselNext, CarouselPrevious,
    Dialog, DialogContent
} from "@evonaplo/ui-library";
import { useApiClient } from "../../hooks/use-api-client";

export default function SpreadsheetImport() {
    const apiClient = useApiClient();
    const [status, setStatus] = useState<"idle" | "loading" | "valid" | "invalid" | "done">("idle");
    const [rows, setRows] = useState<any[]>([]);
    const fileInputRef = useRef<HTMLInputElement>(null);

    const playSound = (type: "error" | "success" | "loading")=> {
        const audio = new Audio(`/sounds/${type}.mp3`);
        audio.play().catch(error => console.warn("Sound blocked by browser: ", error));
    }
    useEffect(() => {
        if (status === "invalid") {
            playSound("error");
        }
        if (status === "done") {
            playSound("success");
        }
        if (status === "loading") {
            playSound("loading");
        }
        
        if (status === "invalid" || status === "done") {
            const timer = setTimeout(() => setStatus("idle"), 3500);
            return () => clearTimeout(timer);
        }
    }, [status]);

    const handleFileUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0];
        if (!file) return;

        e.target.value = "";
        setStatus("loading");

        if (!file.name.endsWith(".xlsx") && !file.name.endsWith(".csv")) {
            setStatus("invalid");
            return;
        }

        try {
            await new Promise((resolve) => setTimeout(resolve, 1200));
            const response = await apiClient.data.dataImportPost(file);
            const importedData = response.data;

            if (importedData && importedData.length > 0) {
                const mappedRows = importedData.map((item: any, index: number) => ({
                    id: index + 1,
                    ts: item.timestamp
                        ? new Date(item.timestamp).toLocaleString("hu-HU")
                        : "-",
                    name: item.name || "-",
                    email: item.email || "-",
                    phone: item.phoneNumber || "-",
                    program: item.major || "-",
                    firstTime: item.isFirstTime || "-",
                    goals: item.goals || "-",
                    stayInTeam: item.stayInTeam || "-",
                    comments: item.otherComments || "-"
                }));

                setRows(mappedRows);
                setStatus("valid");
            } else {
                setStatus("invalid");
                setRows([]);
            }
        } catch (error) {
            setStatus("invalid");
            setRows([]);
        }
    };

    // No DB save implemented yet, so delete from frontend and empty list.
    const handleSave = (idToRemove: number) => {
        const newRows = rows.filter((row) => row.id !== idToRemove);
        setRows(newRows);
        if (newRows.length === 0) setStatus("done");
    };

    const handleSaveAll = () => {
        setRows([]);
        setStatus("done");
    };

    const isDialogOpen = status === "valid" && rows.length > 0;

    return (
        <>
            <input type="file" accept=".csv, .xlsx" className="hidden" ref={fileInputRef} onChange={handleFileUpload} />

            <div className="fixed top-6 left-1/2 -translate-x-1/2 z-[100] w-full max-w-md pointer-events-none flex flex-col gap-2 transition-all duration-300">
                <div className={`absolute w-full transition-all duration-500 ease-in-out ${status === "loading" ? "translate-y-0 opacity-100 visible" : "-translate-y-24 opacity-0 invisible"}`}>
                    <Alert className="bg-background/95 backdrop-blur shadow-xl">
                        <Loader2 className="animate-spin h-4 w-4" />
                        <AlertTitle>Importing...</AlertTitle>
                        <AlertDescription>Importing records from spreadsheet.</AlertDescription>
                    </Alert>
                </div>
                <div className={`absolute w-full transition-all duration-500 ease-in-out ${status === "invalid" ? "translate-y-0 opacity-100 visible" : "-translate-y-24 opacity-0 invisible"}`}>
                    <Alert variant="destructive" className="bg-background/95 backdrop-blur shadow-xl">
                        <AlertCircle className="h-4 w-4" />
                        <AlertTitle>Spreadsheet error</AlertTitle>
                        <AlertDescription>There was an error with your import. Please check your file format.</AlertDescription>
                    </Alert>
                </div>
                <div className={`absolute w-full transition-all duration-500 ease-in-out ${status === "done" ? "translate-y-0 opacity-100 visible" : "-translate-y-24 opacity-0 invisible"}`}>
                    <Alert className="border-green-500 text-green-500 bg-background/95 backdrop-blur shadow-xl">
                        <CheckCircle2Icon className="h-4 w-4" color="currentColor" />
                        <AlertTitle>Import successful</AlertTitle>
                        <AlertDescription>Records successfully imported from spreadsheet.</AlertDescription>
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
                    onInteractOutside={(e: any) => e.preventDefault()}
                >
                    <Carousel className="w-[850px] min-w-[850px] max-w-[850px] relative outline-none focus:outline-none">
                        <CarouselContent className="ml-0 outline-none focus:outline-none">
                            {rows.map((r) => (
                                <CarouselItem key={r.id} className="pl-0 basis-full w-[850px] min-w-[850px] max-w-[850px] flex justify-center pb-4 pt-4 outline-none focus:outline-none">
                                    <Card className="w-[750px] min-w-[750px] max-w-[750px] h-[550px] shrink-0 border border-border/50 bg-background shadow-2xl rounded-2xl p-8 flex flex-col gap-6 outline-none focus:outline-none">

                                        <div className="border-b border-border/40 pb-4">
                                            <h2 className="text-3xl font-bold tracking-tight">{r.name}</h2>
                                        </div>

                                        <div className="grid grid-cols-2 gap-y-6 gap-x-8 text-sm max-h-[350px] overflow-y-auto pr-2">
                                            <div className="space-y-1"><p className="text-muted-foreground font-semibold">Időbélyeg</p><p className="font-medium text-foreground">{r.ts}</p></div>
                                            <div className="space-y-1"><p className="text-muted-foreground font-semibold">Email cím</p><p className="font-medium text-foreground">{r.email}</p></div>
                                            <div className="space-y-1"><p className="text-muted-foreground font-semibold">Telefonszám</p><p className="font-medium text-foreground">{r.phone}</p></div>
                                            <div className="space-y-1"><p className="text-muted-foreground font-semibold">Évfolyam és szak</p><p className="font-medium text-foreground">{r.program}</p></div>
                                            <div className="space-y-1"><p className="text-muted-foreground font-semibold">Először vesz-e részt?</p><p className="font-medium text-foreground">{r.firstTime}</p></div>
                                            <div className="space-y-1"><p className="text-muted-foreground font-semibold">Csapatban marad-e?</p><p className="font-medium text-foreground">{r.stayInTeam}</p></div>

                                            <div className="col-span-2 space-y-1 mt-2 p-3 bg-muted/30 rounded-lg">
                                                <p className="text-muted-foreground font-semibold">Célok</p>
                                                <p className="font-medium text-foreground italic">"{r.goals}"</p>
                                            </div>
                                        </div>

                                        <div className="flex justify-between gap-4 mt-auto pt-4 border-t border-border/40">
                                            <Button
                                                variant="secondary"
                                                className="w-36 cursor-pointer shadow-md"
                                                onClick={handleSaveAll}
                                            >
                                                Save all
                                            </Button>
                                            <Button
                                                className="bg-green-600 hover:bg-green-700 w-36 rounded-full font-bold cursor-pointer text-white shadow-md"
                                                onClick={() => handleSave(r.id)}
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