import { Checkbox } from "@evonaplo/ui-library";
import type { FieldConfig } from "./index";

interface FormCheckboxProps<T> {
  field: FieldConfig<T>;
  value: boolean;
  isEditing: boolean;
  onChange: (key: keyof T, checked: boolean) => void;
}

export function FormCheckbox<T>({ field, value, isEditing, onChange }: FormCheckboxProps<T>) {
  const checkboxId = `checkbox-${String(field.key)}`;

  return (
    <div
      className={`flex items-center gap-3 px-4 py-3 rounded-xl border transition-all select-none w-full
        ${isEditing
          ? "bg-background border-primary/20 shadow-sm hover:border-primary/50 cursor-pointer group"
          : "bg-transparent border-transparent cursor-default"
        }`}
      onClick={() => {
        if (!isEditing) return;
        onChange(field.key, !value);
      }}
    >
      <Checkbox
        id={checkboxId}
        checked={value}
        disabled={!isEditing}
        onClick={(e: React.MouseEvent) => e.stopPropagation()}
        onCheckedChange={(checked: boolean) => onChange(field.key, checked === true)}
        className={`transition-colors ${
          isEditing
            ? "border-muted-foreground group-hover:border-primary data-[state=checked]:bg-primary"
            : "opacity-70"
        }`}
      />

      <div className="grid gap-1.5 leading-none pointer-events-none">
        <label
          htmlFor={checkboxId}
          className={`text-sm font-medium transition-colors ${
            isEditing
              ? "text-foreground font-semibold group-hover:text-primary"
              : "text-muted-foreground"
          }`}
        >
          {field.label}
        </label>
      </div>
    </div>
  );
}
