import { useState, Suspense, useTransition, useMemo } from "react";
import { Button } from "@evonaplo/ui-library";
import { Plus, SlidersHorizontal } from "lucide-react";
import { useApiClient } from "../../hooks/use-api-client";
import type { StudentDTO } from "../../api";
import { GenericDialog, type FieldConfig } from "src/components/admin/GenericDialog";
import StudentsList from "../../components/admin/StudentsList";

const studentFields: FieldConfig<StudentDTO>[] = [
  { key: "name", label: "Name", type: "text" },
  { key: "phoneNumber", label: "Phone", type: "text" },
  { key: "email", label: "Email", type: "text", fullWidth: true },
  { key: "currentSemester", label: "Semester", type: "number" },
  { key: "scholarshipDurationInSemesters", label: "Scholarship Duration", type: "number" },
  { key: "universityProgramme", label: "University Programme", type: "text", fullWidth: true },
  { key: "personalGoals", label: "Personal Goals", type: "text", fullWidth: true },
  { key: "isInTheirFirstSemester", label: "Is in their first semester", type: "checkbox" },
  { key: "hasAppliedForScholarship", label: "Has applied for scholarship", type: "checkbox" },
  { key: "hasScholarship", label: "Has scholarship", type: "checkbox" },
  { key: "hasAppliedForInternship", label: "Has applied for internship", type: "checkbox" },
  { key: "hasInternship", label: "Has internship", type: "checkbox" },
  { key: "isWorkingStudent", label: "Is working student", type: "checkbox" },
  { key: "workExperienceInSemesters", label: "Work Experience", type: "number", fullWidth: true },
  { key: "wantsToStayWithCurrentTeam", label: "Wants to stay with current team", type: "checkbox" },
];

export default function StudentsPage() {
  const apiClient = useApiClient();
  const [isPending, startTransition] = useTransition();

  const initialPromise = useMemo(() => {
    return (async () => {
      const res = await apiClient.students.apiStudentsGet();
      return res.data;
    })();
  }, [apiClient]);

  const [studentsPromise, setStudentsPromise] = useState<Promise<StudentDTO[]>>(initialPromise);

  const [selectedStudent, setSelectedStudent] = useState<StudentDTO | null>(null);
  const [isDialogOpen, setIsDialogOpen] = useState(false);
  const [isCreating, setIsCreating] = useState(false);

  const triggerRefresh = () => {
    const promise = apiClient.students.apiStudentsGet().then(res => res.data);
    setStudentsPromise(promise);
    return promise;
  };

  const handleEdit = (student: StudentDTO) => {
    setSelectedStudent(student);
    setIsCreating(false);
    setIsDialogOpen(true);
  };

  const handleAdd = () => {
    setSelectedStudent({
      name: "",
      email: "",
      phoneNumber: "",
      universityProgramme: "",
      currentSemester: 0,
      isInTheirFirstSemester: false,
      personalGoals: "",
      hasAppliedForScholarship: false,
      hasScholarship: false,
      scholarshipDurationInSemesters: 0,
      hasAppliedForInternship: false,
      hasInternship: false,
      isWorkingStudent: false,
      workExperienceInSemesters: 0,
      wantsToStayWithCurrentTeam: false,
    } as StudentDTO);
    setIsCreating(true);
    setIsDialogOpen(true);
  };

  const handleSave = async (student: StudentDTO) => {
    try {
      if (isCreating) {
        await apiClient.students.apiStudentsPost(student);
      } else {
        await apiClient.students.apiStudentsIdPut(student.id!, student);
      }

      setIsDialogOpen(false);
      startTransition(async () => {
        await triggerRefresh();
      });
    } catch (error) {
      console.error("Unsuccessful save:", error);
      throw error;
    }
  };

  const handleDelete = (id: string) => {
    if (!confirm("Are you sure you want to delete this student?")) return;

    startTransition(async () => {
      try {
        await apiClient.students.apiStudentsIdDelete(id);
        await triggerRefresh();
      } catch (error) {
        console.error("Unsuccessful delete:", error);
      }
    });
  };

  return (
    <div className="max-w-6xl w-full mx-auto py-4">
      <div className="flex items-center justify-between mb-8">
        <h1 className="text-3xl font-bold tracking-tight">Students</h1>
        <div className="flex items-center gap-6">
          <button className="flex items-center gap-2 text-muted-foreground hover:text-foreground transition-colors">
            <span className="text-sm font-medium">Filters</span>
            <SlidersHorizontal className="w-4 h-4" />
          </button>
          <Button onClick={handleAdd} className="bg-primary hover:bg-primary/90 text-primary-foreground px-5 h-10 rounded-lg">
            <Plus className="w-4 h-4 mr-1" />
            Add
          </Button>
        </div>
      </div>

      <Suspense fallback={<div className="text-center p-8 text-muted-foreground">Loading students...</div>}>
        <div className={isPending ? "opacity-50 pointer-events-none transition-opacity" : "transition-opacity"}>
          <StudentsList studentsPromise={studentsPromise} onEdit={handleEdit} onDelete={handleDelete} />
        </div>
      </Suspense>

      <GenericDialog<StudentDTO>
        title="Student Details"
        item={selectedStudent}
        fields={studentFields}
        isOpen={isDialogOpen}
        onClose={() => setIsDialogOpen(false)}
        onSave={handleSave}
        isCreating={isCreating}
      />
    </div>
  );
}
