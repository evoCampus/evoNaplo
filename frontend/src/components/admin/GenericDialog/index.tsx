import { useState } from "react";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  Button,
} from "@evonaplo/ui-library";
import { Pencil, Save, X } from "lucide-react";
import { FormField } from "./FormField";
import { FormCheckbox } from "./FormCheckbox";

export interface FieldConfig<T> {
  key: keyof T;
  label: string;
  type?: "text" | "number" | "checkbox";
  fullWidth?: boolean;
}

interface GenericDialogProps<T> {
  title: string;
  item: T | null;
  fields: FieldConfig<T>[];
  isOpen: boolean;
  onClose: () => void;
  onSave: (item: T) => Promise<void>;
  isCreating?: boolean;
}

export function GenericDialog<T extends object>({
    title,
    item,
    fields,
    isOpen,
    onClose,
    onSave,
    isCreating = false,
}: GenericDialogProps<T>) {
    const [isEditing, setIsEditing] = useState(isCreating);
    const [formData, setFormData] = useState<Partial<T>>(item ? { ...item } : {});
    const [isSaving, setIsSaving] = useState<boolean>(false);

    const [prevIsOpen, setPrevIsOpen] = useState<boolean>(isOpen);
    const [prevItem, setPrevItem] = useState(item);

    if (isOpen !== prevIsOpen || item !== prevItem) {
        setPrevIsOpen(isOpen);
        setPrevItem(item);
        if (isOpen) {
            setIsEditing(isCreating);
            setFormData(item ? { ...item } : {});
        }
    }

    const handleValueChange = (key: keyof T, value: unknown) => {
        setFormData((prev) => ({ ...prev, [key]: value }));
    };

    const handleSave = async () => {
        setIsSaving(true);
        try {
            await onSave(formData as T);
            setIsEditing(false);
            if (isCreating) {
                onClose();
            }
        } catch (error: unknown) {
            console.error(error);
        } finally {
            setIsSaving(false);
        }
    };

    const handleCancelEdit = () => {
        if (isCreating) {
            onClose();
        } else {
            setIsEditing(false);
            setFormData(item ? { ...item } : {});
        }
    };

    const displayTitle = !isCreating && item && "name" in item ? String(item.name) : title;

    return (
        <Dialog open={isOpen} onOpenChange={(open: boolean) => !open && onClose()}>
            <DialogContent
                className="max-w-md w-full sm:max-w-2xl bg-secondary text-foreground border-none shadow-lg rounded-3xl p-4 sm:p-8 flex flex-col max-h-[90dvh] overflow-hidden"
                showCloseButton={false}
                onOpenAutoFocus={(e: FocusEvent) => e.preventDefault()}
            >
                {/* Header */}
                <DialogHeader className="flex flex-row justify-between items-start mb-4 shrink-0">
                    <div className="flex flex-col gap-1">
                        <DialogTitle className="text-xl sm:text-2xl font-bold text-foreground">
                            {isCreating ? `Create ${title}` : displayTitle}
                        </DialogTitle>
                        <DialogDescription className="sr-only">
                            {isCreating ? `Form to create a new ${title}` : `Details and editing form for ${displayTitle}`}
                        </DialogDescription>
                    </div>
                    <Button variant="ghost" size="icon" onClick={onClose} className="hover:bg-accent/20 hover:text-accent-foreground hover:cursor-pointer rounded-full shrink-0">
                        <X className="w-6 h-6 text-muted-foreground" />
                    </Button>
                </DialogHeader>

                {/* Scrollable body */}
                <div className="flex-1 overflow-y-auto pr-2 scrollbar-thin scrollbar-thumb-muted-foreground/20 scrollbar-track-transparent">
                    <div className="grid grid-cols-1 sm:grid-cols-2 gap-4 pb-4">
                        {fields
                            .filter((field) => field.type !== "checkbox")
                            .map((field) => (
                                <FormField
                                    key={String(field.key)}
                                    field={field}
                                    value={formData[field.key]}
                                    isEditing={isEditing}
                                    onChange={handleValueChange}
                                />
                            ))}
                    </div>

                    {/* Checkboxes */}
                    <div className="flex flex-col gap-3 pb-6 border-t border-foreground/5 pt-4">
                        <h3 className="text-sm font-semibold text-muted-foreground mb-1">Additional details</h3>
                        <div className="grid grid-cols-1 sm:grid-cols-2 gap-3">
                            {fields
                                .filter((field) => field.type === "checkbox")
                                .map((field) => (
                                    <FormCheckbox
                                        key={String(field.key)}
                                        field={field}
                                        value={!!formData[field.key]}
                                        isEditing={isEditing}
                                        onChange={handleValueChange}
                                    />
                                ))}
                        </div>
                    </div>
                </div>

                {/* Footer */}
                <div className="mt-4 pt-4 border-t border-foreground/5 flex flex-row justify-end items-center gap-3 shrink-0">
                    {!isEditing ? (
                        <Button
                            onClick={() => setIsEditing(true)}
                            className="bg-primary text-primary-foreground cursor-pointer hover:bg-primary/90 rounded-xl px-6 py-6 h-auto shadow-md flex items-center gap-2 whitespace-nowrap"
                        >
                            <Pencil className="w-4 h-4" />
                            Edit
                        </Button>
                    ) : (
                        <>
                            <Button
                                variant="ghost"
                                onClick={handleCancelEdit}
                                disabled={isSaving}
                                className="hover:bg-foreground/5 cursor-pointer text-muted-foreground rounded-xl px-6 py-6 h-auto whitespace-nowrap transition-none"
                            >
                                Cancel
                            </Button>
                            <Button
                                onClick={handleSave}
                                disabled={isSaving}
                                className="bg-primary text-primary-foreground cursor-pointer hover:bg-primary/90 rounded-xl px-8 py-6 h-auto shadow-md flex items-center gap-2 whitespace-nowrap disabled:opacity-70 disabled:cursor-not-allowed"
                            >
                                {isSaving ? "Saving..." : "Save"}
                                <Save className="w-4 h-4" />
                            </Button>
                        </>
                    )}
                </div>
            </DialogContent>
        </Dialog>
    );
}
