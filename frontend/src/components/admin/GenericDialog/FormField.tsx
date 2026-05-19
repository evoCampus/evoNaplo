import type { FieldConfig } from "./index";

interface FormFieldProps<T> {
  field: FieldConfig<T>;
  value: unknown;
  isEditing: boolean;
  onChange: (key: keyof T, val: unknown) => void;
}

export function FormField<T>({ field, value, isEditing, onChange }: FormFieldProps<T>) {
  const inputType = field.type || "text";
  const isFullWidth = field.fullWidth || inputType === "checkbox";
  const displayValue = value !== undefined && value !== null ? String(value) : "";

  return (
    <div className={`flex flex-col gap-1 w-full ${isFullWidth ? "sm:col-span-2" : ""}`}>
      <div className="flex items-center gap-2 bg-background rounded-xl px-4 py-3 shadow-sm border border-transparent focus-within:ring-2 focus-within:ring-ring focus-within:ring-offset-2 transition-all w-full">
          <span className="text-sm font-medium text-muted-foreground whitespace-nowrap">
            {field.label}: {field.required && <span className="text-destructive">*</span>}
          </span>
        {isEditing ? (
          <input
            type={inputType}
            value={displayValue}
            onChange={(e) => {
              const val = inputType === "number" ? Number(e.target.value) : e.target.value;
              onChange(field.key, val);
            }}
            className="flex-1 bg-transparent border-none outline-none text-foreground text-sm p-0 focus:ring-0 min-w-0"
          />
        ) : (
          <span className="text-sm text-foreground truncate">{displayValue}</span>
        )}
      </div>
    </div>
  );
}
