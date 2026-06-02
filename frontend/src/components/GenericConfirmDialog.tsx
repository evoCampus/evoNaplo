import {
    Dialog,
    DialogContent,
    DialogHeader,
    DialogTitle,
    DialogDescription,
    Button,
} from "@evonaplo/ui-library";
import { AlertCircle } from "lucide-react";

interface GenericConfirmDialogProps {
    isOpen: boolean;
    onClose: () => void;
    onConfirm: () => void;
    title: string;
    description: string;
    confirmText?: string;
    cancelText?: string;
    isPending?: boolean;
    variant?: "destructive" | "primary";
    error?: string | null;
}

export default function GenericConfirmDialog({
    isOpen,
    onClose,
    onConfirm,
    title,
    description,
    confirmText = "Confirm",
    cancelText = "Cancel",
    isPending = false,
    variant = "destructive",
    error = null,
}: GenericConfirmDialogProps) {
    return (
        <Dialog
            open={isOpen}
            onOpenChange={(open: boolean) => !open && onClose()}
        >
            <DialogContent
                className="max-w-sm w-full bg-secondary text-foreground border-none shadow-lg rounded-3xl p-6"
                showCloseButton={false}
            >
                <DialogHeader className="mb-4">
                    <DialogTitle className="text-xl font-bold text-foreground">
                        {title}
                    </DialogTitle>
                    <DialogDescription className="text-muted-foreground mt-2">
                        {description}
                    </DialogDescription>
                </DialogHeader>

                {error && (
                    <div className="mt-4 p-3 rounded-xl bg-destructive/10 border border-destructive/20 flex items-start gap-2.5 text-destructive">
                        <AlertCircle className="w-5 h-5 shrink-0 mt-0.5" />
                        <span className="text-sm font-medium leading-tight">
                            {error}
                        </span>
                    </div>
                )}

                <div className="flex justify-end gap-3 mt-6">
                    <Button
                        variant="ghost"
                        onClick={onClose}
                        disabled={isPending}
                        className="hover:bg-foreground/5 cursor-pointer text-muted-foreground rounded-xl px-6"
                    >
                        {cancelText}
                    </Button>
                    <Button
                        onClick={onConfirm}
                        disabled={isPending}
                        className={`cursor-pointer rounded-xl px-6 shadow-md transition-all ${
                            variant === "destructive"
                                ? "bg-destructive text-destructive-foreground hover:bg-destructive/90"
                                : "bg-primary text-primary-foreground hover:bg-primary/90"
                        }`}
                    >
                        {isPending ? "Processing..." : confirmText}
                    </Button>
                </div>
            </DialogContent>
        </Dialog>
    );
}
