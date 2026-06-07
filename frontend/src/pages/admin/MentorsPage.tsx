import { useState, useTransition, useMemo, useEffect } from "react";
import { useLocation, useNavigate } from "react-router";
import { Button } from "@evonaplo/ui-library";
import { Plus } from "lucide-react";
import { useApiClient } from "../../hooks/use-api-client";
import type { MentorDTO } from "../../api";
import { GenericDialog, type FieldConfig } from "src/components/admin/GenericDialog";
import MentorsList from "../../components/admin/MentorsList";
import GenericConfirmDialog from "src/components/GenericConfirmDialog";
import ErrorBoundary from "../../components/ErrorBoundary";
import { AdminFilter, type FilterField } from "../../components/admin/AdminFilter";

const mentorFields: FieldConfig<MentorDTO>[] = [
  { key: "name", label: "Name", type: "text", required: true },
  { key: "email", label: "Email", type: "text", fullWidth: true, required: true },
  { key: "phoneNumber", label: "Phone", type: "text", required: true },
  { key: "mentorProfile", label: "Mentor Profile", type: "text", fullWidth: true },
  { key: "semesterNumber", label: "Semester Number", type: "number", required: true },
  { key: "isActive", label: "Is Active", type: "checkbox" },
];

const mentorFilterFields: FilterField<MentorDTO>[] = [
  { key: "name", label: "Name", type: "text" },
  { key: "email", label: "Email", type: "text" },
  { key: "isActive", label: "Is Active", type: "boolean" },
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

  const [allMentors, setAllMentors] = useState<MentorDTO[] | null>(null);
  const [filters, setFilters] = useState<Partial<Record<keyof MentorDTO, unknown>>>({});

  const initialPromise = useMemo(() => {
    return apiClient.mentors.apiMentorsGet().then((res) => res.data);
  }, [apiClient]);

  useEffect(() => {
    let cancelled = false;
    initialPromise
      .then(data => { if (!cancelled) setAllMentors(data); })
      .catch(err => { if (!cancelled) console.error("Failed to load mentors:", err); });
    return () => { cancelled = true; };
  }, [initialPromise]);

  const mentorsPromise = useMemo(() => {
    if (allMentors === null) return initialPromise;

    const filtered = allMentors.filter((m) => {
      return Object.entries(filters).every(([key, value]) => {
        if (value === undefined || value === null || value === "") return true;
        const itemValue = m[key as keyof MentorDTO];
        if (typeof value === "string") {
          return itemValue?.toString().toLowerCase().includes(value.toLowerCase());
        }
        return itemValue === value;
      });
    });
    return Promise.resolve(filtered);
  }, [allMentors, filters, initialPromise]);

  const shouldOpenAdd = location.state?.openAdd === true;
  const editItem = location.state?.editItem as MentorDTO | undefined;

  const [selectedMentor, setSelectedMentor] = useState<MentorDTO | null>(
    editItem ? editItem : shouldOpenAdd ? DEFAULT_MENTOR : null
  );
  const [isDialogOpen, setIsDialogOpen] = useState(shouldOpenAdd || !!editItem);
  const [isCreating, setIsCreating] = useState(shouldOpenAdd && !editItem);

  const [mentorToDelete, setMentorToDelete] = useState<string | null>(null);
  const [deleteError, setDeleteError] = useState<string | null>(null);

  const triggerRefresh = async () => {
    try {
      const res = await apiClient.mentors.apiMentorsGet();
      setAllMentors(res.data);
      return res.data;
    } catch (error) {
      console.error("Failed to refresh mentors:", error);
      throw error;
    }
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
    if (location.state?.openAdd || location.state?.editItem) {
      navigate(location.pathname, { replace: true, state: {} });
    }
  }, [location.state?.openAdd, location.state?.editItem, location.pathname, navigate]);

  const handleSave = async (mentor: MentorDTO) => {
    try {
      if (isCreating) {
        await apiClient.mentors.apiMentorsPost(mentor);
      } else {
        if (!mentor.id) throw new Error("Mentor ID is missing");
        await apiClient.mentors.apiMentorsIdPut(mentor.id, mentor);
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
        <h1 className="text-3xl font-bold tracking-tight text-foreground">Mentors</h1>
        <div className="flex items-center gap-6">
          <AdminFilter
            fields={mentorFilterFields}
            currentFilters={filters}
            onFilterChange={setFilters}
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
          <MentorsList mentorsPromise={mentorsPromise} onEdit={handleEdit} onDelete={handleDeleteRequest} />
        </div>
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
