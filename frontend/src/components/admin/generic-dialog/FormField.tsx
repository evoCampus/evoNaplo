import { useState } from "react";
import type { FieldConfig } from "./GenericDialog";

interface FormFieldProps<T> {
  field: FieldConfig<T>;
  value: unknown;
  isEditing: boolean;
  onChange: (key: keyof T, val: unknown) => void;
}

function validate<T>(field: FieldConfig<T>, value: unknown): string | null {
  const inputType = field.type || "text";

  if (field.required && (value === undefined || value === null || String(value).trim() === "")) {
    return `${field.label} is required.`;
  }

  if (inputType === "number" && value !== "" && isNaN(Number(value))) {
    return `${field.label} must be a valid number.`;
  }

  return null;
}

export function FormField<T>({ field, value, isEditing, onChange }: FormFieldProps<T>) {
  const inputType = field.type || "text";
  const isFullWidth = field.fullWidth ?? false;
  const displayValue = value !== undefined && value !== null ? String(value) : "";

  const [error, setError] = useState<string | null>(null);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const raw = e.target.value;
    const val = inputType === "number" ? (raw === "" ? "" : Number(raw)) : raw;
    onChange(field.key, val);
    if (error) setError(validate(field, raw));
  };

  const handleBlur = (e: React.FocusEvent<HTMLInputElement>) => {
    setError(validate(field, e.target.value));
  };

  return (
    <div className={`flex flex-col gap-1 w-full ${isFullWidth ? "sm:col-span-2" : ""}`}>
      <div className={`flex items-center gap-2 bg-background rounded-xl px-4 py-3 shadow-sm border transition-all w-full
        ${error ? "border-destructive focus-within:ring-2 focus-within:ring-destructive focus-within:ring-offset-2" : "border-transparent focus-within:ring-2 focus-within:ring-ring focus-within:ring-offset-2"}`}
      >
        <span className="text-sm font-medium text-muted-foreground whitespace-nowrap">
          {field.label}: {field.required && <span className="text-destructive">*</span>}
        </span>
        <input
          type={inputType}
          value={displayValue}
          disabled={!isEditing}
          onChange={handleChange}
          onBlur={handleBlur}
          className="flex-1 bg-transparent border-none outline-none text-foreground text-sm p-0 focus:ring-0 min-w-0 disabled:cursor-default disabled:text-foreground"
        />
      </div>
      {error && (
        <span className="text-xs text-destructive px-1">{error}</span>
      )}
    </div>
  );
}
