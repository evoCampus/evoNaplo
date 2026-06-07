import { useState, useTransition, useMemo, useEffect } from "react";
import { useLocation, useNavigate } from "react-router";
import { Button } from "@evonaplo/ui-library";
import { Plus } from "lucide-react";
import { useApiClient } from "../../hooks/use-api-client";
import type { StudentDTO } from "../../api";
import { GenericDialog, type FieldConfig } from "src/components/admin/generic-dialog/GenericDialog";
import StudentsList from "../../components/admin/StudentsList";
import GenericConfirmDialog from "src/components/GenericConfirmDialog";
import ErrorBoundary from "../../components/ErrorBoundary";
import { AdminFilter, type FilterField } from "../../components/admin/AdminFilter";

const studentFields: FieldConfig<StudentDTO>[] = [
  { key: "name", label: "Name", type: "text", required: true },
  { key: "phoneNumber", label: "Phone", type: "text", required: true },
  { key: "email", label: "Email", type: "text", fullWidth: true, required: true },
  { key: "currentSemester", label: "Semester", type: "number", required: true },
  { key: "scholarshipDurationInSemesters", label: "Scholarship Duration", type: "number", required: true },
  { key: "universityProgramme", label: "University Programme", type: "text", fullWidth: true, required: true },
  { key: "personalGoals", label: "Personal Goals", type: "text", fullWidth: true, required: true },
  { key: "isInTheirFirstSemester", label: "Is in their first semester", type: "checkbox" },
  { key: "hasAppliedForScholarship", label: "Has applied for scholarship", type: "checkbox" },
  { key: "hasScholarship", label: "Has scholarship", type: "checkbox" },
  { key: "hasAppliedForInternship", label: "Has applied for internship", type: "checkbox" },
  { key: "hasInternship", label: "Has internship", type: "checkbox" },
  { key: "isWorkingStudent", label: "Is working student", type: "checkbox" },
  { key: "workExperienceInSemesters", label: "Work Experience", type: "number", fullWidth: true, required: true },
  { key: "wantsToStayWithCurrentTeam", label: "Wants to stay with current team", type: "checkbox" },
];

const studentFilterFields: FilterField<StudentDTO>[] = [
  { key: "name", label: "Name", type: "text" },
  { key: "email", label: "Email", type: "text" },
  { key: "universityProgramme", label: "Programme", type: "text" },
  { key: "isWorkingStudent", label: "Working Student", type: "boolean" },
];

const DEFAULT_STUDENT: StudentDTO = {
  id: null,
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
};

export default function StudentsPage() {
  const apiClient = useApiClient();
  const location = useLocation();
  const navigate = useNavigate();
  const [isPending, startTransition] = useTransition();

  const [allStudents, setAllStudents] = useState<StudentDTO[] | null>(null);
  const [filters, setFilters] = useState<Partial<Record<keyof StudentDTO, unknown>>>({});

  const initialPromise = useMemo(() => {
    return apiClient.students.apiStudentsGet().then((res) => res.data);
  }, [apiClient]);

  useEffect(() => {
    let cancelled = false;
    initialPromise
      .then(data => { if (!cancelled) setAllStudents(data); })
      .catch(err => { if (!cancelled) console.error("Failed to load students:", err); });
    return () => { cancelled = true; };
  }, [initialPromise]);

  const studentsPromise = useMemo(() => {
    if (allStudents === null) return initialPromise;

    const filtered = allStudents.filter((s) => {
      return Object.entries(filters).every(([key, value]) => {
        if (value === undefined || value === null || value === "") return true;
        const itemValue = s[key as keyof StudentDTO];
        if (typeof value === "string") {
          return itemValue?.toString().toLowerCase().includes(value.toLowerCase());
        }
        return itemValue === value;
      });
    });
    return Promise.resolve(filtered);
  }, [allStudents, filters, initialPromise]);

  const filteredStudentCount = useMemo(() => {
    if (allStudents === null) return undefined;
    return allStudents.filter((s) => {
      return Object.entries(filters).every(([key, value]) => {
        if (value === undefined || value === null || value === "") return true;
        const itemValue = s[key as keyof StudentDTO];
        if (typeof value === "string") {
          return itemValue?.toString().toLowerCase().includes(value.toLowerCase());
        }
        return itemValue === value;
      });
    }).length;
  }, [allStudents, filters]);

  const shouldOpenAdd = location.state?.openAdd === true;
  const editItem = location.state?.editItem as StudentDTO | undefined;

  const [selectedStudent, setSelectedStudent] = useState<StudentDTO | null>(
    editItem ? editItem : shouldOpenAdd ? DEFAULT_STUDENT : null
  );
  const [isDialogOpen, setIsDialogOpen] = useState(shouldOpenAdd || !!editItem);
  const [isCreating, setIsCreating] = useState(shouldOpenAdd && !editItem);

  const [studentToDelete, setStudentToDelete] = useState<string | null>(null);
  const [deleteError, setDeleteError] = useState<string | null>(null);

  const triggerRefresh = async () => {
    try {
      const res = await apiClient.students.apiStudentsGet();
      setAllStudents(res.data);
      return res.data;
    } catch (error) {
      console.error("Failed to refresh students:", error);
      throw error;
    }
  };

  const handleEdit = (student: StudentDTO) => {
    setSelectedStudent(student);
    setIsCreating(false);
    setIsDialogOpen(true);
  };

  const handleAdd = () => {
    setSelectedStudent(DEFAULT_STUDENT);
    setIsCreating(true);
    setIsDialogOpen(true);
  };

  useEffect(() => {
    if (location.state?.openAdd || location.state?.editItem) {
      navigate(location.pathname, { replace: true, state: {} });
    }
  }, [location.state?.openAdd, location.state?.editItem, location.pathname, navigate]);

  const handleSave = async (student: StudentDTO) => {
    try {
      if (isCreating) {
        await apiClient.students.apiStudentsPost(student);
      } else {
        if (!student.id) throw new Error("Student ID is missing");
        await apiClient.students.apiStudentsIdPut(student.id, student);
      }

      setIsDialogOpen(false);
      startTransition(async () => {
        try {
          await triggerRefresh();
        } catch (error) {
          console.error("Failed to refresh after save:", error);
        }
      });
    } catch (error) {
      console.error("Unsuccessful save:", error);
      throw error;
    }
  };

  const handleDeleteRequest = (id: string) => {
    setDeleteError(null);
    setStudentToDelete(id);
  };

  const confirmDelete = () => {
    if (!studentToDelete) return;
    setDeleteError(null);

    startTransition(async () => {
      try {
        await apiClient.students.apiStudentsIdDelete(studentToDelete);
        await triggerRefresh();
        setStudentToDelete(null);
      } catch (error) {
        console.error("Unsuccessful delete:", error);
        setDeleteError("Failed to delete student. Please try again.");
      }
    });
  };

  return (
    <div className="max-w-6xl w-full mx-auto py-4">
      <div className="flex items-center justify-between mb-8">
        <h1 className="text-3xl font-bold tracking-tight text-foreground">Students</h1>
        <div className="flex items-center gap-6">
          <AdminFilter
            fields={studentFilterFields}
            currentFilters={filters}
            onFilterChange={setFilters}
            resultCount={filteredStudentCount}
          />
          <Button
            onClick={handleAdd}
            className="bg-primary hover:bg-primary/90 cursor-pointer text-primary-foreground px-5 h-10 rounded-lg shadow-md transition-all active:scale-95"
          >
            <Plus className="w-4 h-4 mr-1" />
            Add
          </Button>
        </div>
      </div>

      <ErrorBoundary onReset={triggerRefresh}>
        <div
          className={
            isPending
              ? "opacity-50 pointer-events-none transition-opacity duration-200"
              : "transition-opacity duration-200"
          }
        >
          <StudentsList studentsPromise={studentsPromise} onEdit={handleEdit} onDelete={handleDeleteRequest} />
        </div>
      </ErrorBoundary>

      <GenericDialog<StudentDTO>
        title="Student Details"
        item={selectedStudent}
        fields={studentFields}
        isOpen={isDialogOpen}
        onClose={() => setIsDialogOpen(false)}
        onSave={handleSave}
        isCreating={isCreating}
      />

      <GenericConfirmDialog
        isOpen={!!studentToDelete}
        onClose={() => setStudentToDelete(null)}
        onConfirm={confirmDelete}
        title="Delete Student"
        description="Are you sure you want to delete this student? This action cannot be undone."
        confirmText="Delete"
        isPending={isPending}
        variant="destructive"
        error={deleteError}
      />
    </div>
  );
}
