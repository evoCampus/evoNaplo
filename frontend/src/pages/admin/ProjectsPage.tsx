import { useState, useTransition, useMemo, useEffect } from "react";
import { useLocation, useNavigate } from "react-router";
import { Button } from "@evonaplo/ui-library";
import { Plus } from "lucide-react";
import { useApiClient } from "../../hooks/use-api-client";
import type { ProjectDTO } from "../../api";
import { ProjectDialog } from "../../components/admin/ProjectDialog";
import ProjectsList from "../../components/admin/ProjectsList";
import GenericConfirmDialog from "src/components/GenericConfirmDialog";
import ErrorBoundary from "../../components/ErrorBoundary";
import { AdminFilter, type FilterField } from "../../components/admin/AdminFilter";

const projectFilterFields: FilterField<ProjectDTO>[] = [
  { key: "name", label: "Name", type: "text" },
  { key: "description", label: "Description", type: "text" },
];

const DEFAULT_PROJECT: ProjectDTO = {
  id: null,
  name: "",
  description: "",
  teams: [],
  projectLinks: {},
};

export default function ProjectsPage() {
  const apiClient = useApiClient();
  const location = useLocation();
  const navigate = useNavigate();
  const [isPending, startTransition] = useTransition();

  const [allProjects, setAllProjects] = useState<ProjectDTO[] | null>(null);
  const [filters, setFilters] = useState<Partial<Record<keyof ProjectDTO, unknown>>>({});

  const initialPromise = useMemo(() => {
    return apiClient.projects.apiProjectsGet().then((res) => res.data);
  }, [apiClient]);

  useEffect(() => {
    let cancelled = false;
    initialPromise
      .then(data => { if (!cancelled) setAllProjects(data); })
      .catch(err => { if (!cancelled) console.error("Failed to load projects:", err); });
    return () => { cancelled = true; };
  }, [initialPromise]);

  const projectsPromise = useMemo(() => {
    if (allProjects === null) return initialPromise;

    const filtered = allProjects.filter((p) => {
      return Object.entries(filters).every(([key, value]) => {
        if (value === undefined || value === null || value === "") return true;
        const itemValue = p[key as keyof ProjectDTO];
        if (typeof value === "string") {
          return itemValue?.toString().toLowerCase().includes(value.toLowerCase());
        }
        return itemValue === value;
      });
    });
    return Promise.resolve(filtered);
  }, [allProjects, filters, initialPromise]);

  const shouldOpenAdd = location.state?.openAdd === true;
  const editItem = location.state?.editItem as ProjectDTO | undefined;

  const [selectedProject, setSelectedProject] = useState<ProjectDTO | null>(
    editItem ? editItem : shouldOpenAdd ? DEFAULT_PROJECT : null
  );
  const [isDialogOpen, setIsDialogOpen] = useState(shouldOpenAdd || !!editItem);
  const [isCreating, setIsCreating] = useState(shouldOpenAdd && !editItem);

  const [projectToDelete, setProjectToDelete] = useState<string | null>(null);
  const [deleteError, setDeleteError] = useState<string | null>(null);

  const triggerRefresh = async () => {
    try {
      const res = await apiClient.projects.apiProjectsGet();
      setAllProjects(res.data);
      return res.data;
    } catch (error) {
      console.error("Failed to refresh projects:", error);
      throw error;
    }
  };

  const handleEdit = (project: ProjectDTO) => {
    setSelectedProject(project);
    setIsCreating(false);
    setIsDialogOpen(true);
  };

  const handleAdd = () => {
    setSelectedProject(DEFAULT_PROJECT);
    setIsCreating(true);
    setIsDialogOpen(true);
  };

  useEffect(() => {
    if (location.state?.openAdd || location.state?.editItem) {
      navigate(location.pathname, { replace: true, state: {} });
    }
  }, [location.state?.openAdd, location.state?.editItem, location.pathname, navigate]);

  const handleSave = async (project: ProjectDTO) => {
    try {
      if (isCreating) {
        await apiClient.projects.apiProjectsPost(project);
      } else {
        if (!project.id) throw new Error("Project ID is missing");
        await apiClient.projects.apiProjectsIdPut(project.id, project);
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
    setProjectToDelete(id);
  };

  const confirmDelete = () => {
    if (!projectToDelete) return;
    setDeleteError(null);

    startTransition(async () => {
      try {
        await apiClient.projects.apiProjectsIdDelete(projectToDelete);
        await triggerRefresh();
        setProjectToDelete(null);
      } catch (error) {
        console.error("Unsuccessful delete:", error);
        setDeleteError("Failed to delete project. Please try again.");
      }
    });
  };

  return (
    <div className="max-w-6xl w-full mx-auto py-4">
      <div className="flex items-center justify-between mb-8">
        <h1 className="text-3xl font-bold tracking-tight text-foreground">Projects</h1>
        <div className="flex items-center gap-6">
          <AdminFilter
            fields={projectFilterFields}
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
          <ProjectsList projectsPromise={projectsPromise} onEdit={handleEdit} onDelete={handleDeleteRequest} />
        </div>
      </ErrorBoundary>

      <ProjectDialog
        isOpen={isDialogOpen}
        onClose={() => setIsDialogOpen(false)}
        onSave={handleSave}
        item={selectedProject}
        isCreating={isCreating}
      />

      <GenericConfirmDialog
        isOpen={!!projectToDelete}
        onClose={() => setProjectToDelete(null)}
        onConfirm={confirmDelete}
        title="Delete Project"
        description="Are you sure you want to delete this project? This action cannot be undone."
        confirmText="Delete"
        isPending={isPending}
        variant="destructive"
        error={deleteError}
      />
    </div>
  );
}
