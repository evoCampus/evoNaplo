import type { StudentDTO } from "../../api/api";
import { GenericEntityList } from "./GenericEntityList";

export default function StudentsList({
  studentsPromise,
  onEdit,
  onDelete,
}: {
  studentsPromise: Promise<StudentDTO[]>;
  onEdit: (student: StudentDTO) => void;
  onDelete: (id: string) => void;
}) {
  return (
    <GenericEntityList
      dataPromise={studentsPromise}
      onEdit={onEdit}
      onDelete={onDelete}
      renderContent={(student) => (
        <span className="text-lg font-medium text-foreground/90">{student.name || "Unknown"}</span>
      )}
      emptyMessage="No students found."
    />
  );
}
