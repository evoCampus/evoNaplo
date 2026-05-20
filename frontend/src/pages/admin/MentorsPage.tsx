import { useState, Suspense, useTransition, useMemo, useEffect } from "react";
import { useLocation, useNavigate } from "react-router";
import { Button } from "@evonaplo/ui-library";
import { Plus, SlidersHorizontal } from "lucide-react";
import { useApiClient } from "../../hooks/use-api-client";
import type { MentorDTO } from "../../api";
import { GenericDialog, type FieldConfig } from "src/components/admin/GenericDialog";
import MentorsList from "../../components/admin/MentorsList";
import GenericConfirmDialog from "src/components/GenericConfirmDialog";
import ErrorBoundary from "../../components/ErrorBoundary";

const mentorFields: FieldConfig<MentorDTO>[] = [
  { key: "name", label: "Name", type: "text", required: true },
  { key: "email", label: "Email", type: "text", fullWidth: true, required: true },
  { key: "phoneNumber", label: "Phone", type: "text", required: true },
  { key: "mentorProfile", label: "Mentor Profile", type: "text", fullWidth: true },
  { key: "semesterNumber", label: "Semester Number", type: "number", required: true },
  { key: "isActive", label: "Is Active", type: "checkbox" },
];

const DEFAULT_MENTOR: MentorDTO = {
  id: null,
  name: "",
  email: "",
  phoneNumber: "",
  mentorProfile: "",
  semesterNumber: 1,
  isActive: true,
  teams: [],
  projects: [],
};

export default function MentorsPage() {
  const apiClient = useApiClient();
  const location = useLocation();
  const navigate = useNavigate();
  const [isPending, startTransition] = useTransition();

  const initialPromise = useMemo(() => {
    return (async () => {
      const res = await apiClient.mentors.apiMentorsGet();
      return res.data;
    })();
  }, [apiClient]);

  const [mentorsPromise, setMentorsPromise] = useState<Promise<MentorDTO[]>>(initialPromise);

  const shouldOpenAdd = location.state?.openAdd === true;

  const [selectedMentor, setSelectedMentor] = useState<MentorDTO | null>(shouldOpenAdd ? DEFAULT_MENTOR : null);
  const [isDialogOpen, setIsDialogOpen] = useState(shouldOpenAdd);
  const [isCreating, setIsCreating] = useState(shouldOpenAdd);

  const [mentorToDelete, setMentorToDelete] = useState<string | null>(null);
  const [deleteError, setDeleteError] = useState<string | null>(null);

  const triggerRefresh = () => {
    const promise = apiClient.mentors.apiMentorsGet().then(res => res.data);
    setMentorsPromise(promise);
    return promise;
  };

  const handleEdit = (mentor: MentorDTO) => {
    setSelectedMentor(mentor);
    setIsCreating(false);
    setIsDialogOpen(true);
  };

  const handleAdd = () => {
    setSelectedMentor(DEFAULT_MENTOR);
    setIsCreating(true);
    setIsDialogOpen(true);
  };

  useEffect(() => {
    if (location.state?.openAdd) {
      navigate(location.pathname, { replace: true, state: {} });
    }
  }, [location.state?.openAdd, location.pathname, navigate]);

  const handleSave = async (mentor: MentorDTO) => {
    try {
      if (isCreating) {
        await apiClient.mentors.apiMentorsPost(mentor);
      } else {
        await apiClient.mentors.apiMentorsIdPut(mentor.id!, mentor);
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

  const handleDeleteRequest = (id: string) => {
    setDeleteError(null);
    setMentorToDelete(id);
  };

  const confirmDelete = () => {
    if (!mentorToDelete) return;
    setDeleteError(null);

    startTransition(async () => {
      try {
        await apiClient.mentors.apiMentorsIdDelete(mentorToDelete);
        await triggerRefresh();
        setMentorToDelete(null);
      } catch (error) {
        console.error("Unsuccessful delete:", error);
        setDeleteError("Failed to delete mentor. Please try again.");
      }
    });
  };

  return (
    <div className="max-w-6xl w-full mx-auto py-4">
      <div className="flex items-center justify-between mb-8">
        <h1 className="text-3xl font-bold tracking-tight">Mentors</h1>
        <div className="flex items-center gap-6">
          <button className="flex items-center gap-2 text-muted-foreground hover:text-foreground transition-colors">
            <span className="text-sm font-medium">Filters</span>
            <SlidersHorizontal className="w-4 h-4" />
          </button>
          <Button onClick={handleAdd} className="bg-primary hover:bg-primary/90 cursor-pointer text-primary-foreground px-5 h-10 rounded-lg">
            <Plus className="w-4 h-4 mr-1" />
            Add
          </Button>
        </div>
      </div>

      <ErrorBoundary onReset={triggerRefresh}>
        <Suspense fallback={<div className="text-center p-8 text-muted-foreground">Loading mentors...</div>}>
          <div className={isPending ? "opacity-50 pointer-events-none transition-opacity" : "transition-opacity"}>
            <MentorsList mentorsPromise={mentorsPromise} onEdit={handleEdit} onDelete={handleDeleteRequest} />
          </div>
        </Suspense>
      </ErrorBoundary>

      <GenericDialog<MentorDTO>
        title="Mentor Details"
        item={selectedMentor}
        fields={mentorFields}
        isOpen={isDialogOpen}
        onClose={() => setIsDialogOpen(false)}
        onSave={handleSave}
        isCreating={isCreating}
      />

      <GenericConfirmDialog
        isOpen={!!mentorToDelete}
        onClose={() => setMentorToDelete(null)}
        onConfirm={confirmDelete}
        title="Delete Mentor"
        description="Are you sure you want to delete this mentor? This action cannot be undone."
        confirmText="Delete"
        isPending={isPending}
        variant="destructive"
        error={deleteError}
      />
    </div>
  );
}
