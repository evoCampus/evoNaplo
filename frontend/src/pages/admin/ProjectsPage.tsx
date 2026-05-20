import { useState, Suspense, useTransition, useMemo, useEffect } from "react";
import { useLocation, useNavigate } from "react-router";
import { Button } from "@evonaplo/ui-library";
import { Plus, SlidersHorizontal } from "lucide-react";
import { useApiClient } from "../../hooks/use-api-client";
import type { ProjectDTO } from "../../api";
import { ProjectDialog } from "../../components/admin/ProjectDialog";
import ProjectsList from "../../components/admin/ProjectsList";
import GenericConfirmDialog from "src/components/GenericConfirmDialog";
import ErrorBoundary from "../../components/ErrorBoundary";

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

  const initialPromise = useMemo(() => {
    return (async () => {
      const res = await apiClient.projects.apiProjectsGet();
      return res.data;
    })();
  }, [apiClient]);

  const [projectsPromise, setProjectsPromise] = useState<Promise<ProjectDTO[]>>(initialPromise);

  const shouldOpenAdd = location.state?.openAdd === true;

  const [selectedProject, setSelectedProject] = useState<ProjectDTO | null>(shouldOpenAdd ? DEFAULT_PROJECT : null);
  const [isDialogOpen, setIsDialogOpen] = useState(shouldOpenAdd);
  const [isCreating, setIsCreating] = useState(shouldOpenAdd);

  const [projectToDelete, setProjectToDelete] = useState<string | null>(null);
  const [deleteError, setDeleteError] = useState<string | null>(null);

  const triggerRefresh = () => {
    const promise = apiClient.projects.apiProjectsGet().then(res => res.data);
    setProjectsPromise(promise);
    return promise;
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
    if (location.state?.openAdd) {
      navigate(location.pathname, { replace: true, state: {} });
    }
  }, [location.state?.openAdd, location.pathname, navigate]);

  const handleSave = async (project: ProjectDTO) => {
    try {
      if (isCreating) {
        await apiClient.projects.apiProjectsPost(project);
      } else {
        await apiClient.projects.apiProjectsIdPut(project.id!, project);
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
        <h1 className="text-3xl font-bold tracking-tight">Projects</h1>
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
        <Suspense fallback={<div className="text-center p-8 text-muted-foreground">Loading projects...</div>}>
          <div className={isPending ? "opacity-50 pointer-events-none transition-opacity" : "transition-opacity"}>
            <ProjectsList projectsPromise={projectsPromise} onEdit={handleEdit} onDelete={handleDeleteRequest} />
          </div>
        </Suspense>
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
