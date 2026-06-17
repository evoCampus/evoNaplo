import { useState, useEffect } from "react";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  Button
} from "@evonaplo/ui-library";
import { Pencil, Save, X, AlertCircle } from "lucide-react";
import { useApiClient } from "../../hooks/use-api-client";
import type { ProjectDTO, TeamDTO } from "../../api";
import { SearchableCheckboxList } from "./SearchableCheckboxList";
import { getDayName, formatTime } from "../../lib/date-utils";

interface ProjectDialogProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (project: ProjectDTO) => Promise<void>;
  item: ProjectDTO | null;
  isCreating?: boolean;
}

export function ProjectDialog({
  isOpen,
  onClose,
  onSave,
  item,
  isCreating = false,
}: ProjectDialogProps) {
  const apiClient = useApiClient();

  const [prevIsOpen, setPrevIsOpen] = useState(isOpen);
  const [prevItem, setPrevItem] = useState(item);

  const [isEditing, setIsEditing] = useState(isCreating);
  const [name, setName] = useState(item?.name ?? "");
  const [description, setDescription] = useState(item?.description ?? "");
  const [selectedTeams, setSelectedTeams] = useState<string[]>(item?.teams ?? []);

  const [teamSearch, setTeamSearch] = useState("");
  const [teamsList, setTeamsList] = useState<TeamDTO[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [isSaving, setIsSaving] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  if (isOpen !== prevIsOpen || item !== prevItem) {
    setPrevIsOpen(isOpen);
    setPrevItem(item);

    if (isOpen) {
      setIsEditing(isCreating);
      setErrorMessage(null);
      setTeamSearch("");
      setName(item?.name ?? "");
      setDescription(item?.description ?? "");
      setSelectedTeams(item?.teams ?? []);
    }
  }

  useEffect(() => {
    if (!isOpen) return;

    const controller = new AbortController();

    const fetchData = async () => {
      setIsLoading(true);
      try {
        const teamsRes = await apiClient.teams.apiTeamsGet({ signal: controller.signal });
        setTeamsList(teamsRes.data ?? []);
      } catch (error: unknown) {
        if (error instanceof Error && error.name === "CanceledError") return;
        console.error("Failed to load project references:", error);
        setErrorMessage("Failed to load options from the system.");
      } finally {
        if (!controller.signal.aborted) setIsLoading(false);
      }
    };

    fetchData();
    return () => controller.abort();
  }, [isOpen, apiClient]);

  const handleCancelEdit = () => {
    setErrorMessage(null);
    if (isCreating) {
      onClose();
    } else {
      setIsEditing(false);
      if (item) {
        setName(item.name ?? "");
        setDescription(item.description ?? "");
        setSelectedTeams(item.teams ?? []);
      }
    }
  };

  const handleSave = async () => {
    setIsSaving(true);
    setErrorMessage(null);

    if (!name.trim()) {
      setErrorMessage("Project name is required.");
      setIsSaving(false);
      return;
    }

    if (!description.trim()) {
      setErrorMessage("Project description is required.");
      setIsSaving(false);
      return;
    }

    const updatedProject: ProjectDTO = {
      ...item,
      id: item?.id ?? null,
      name,
      description,
      teams: selectedTeams,
      projectLinks: item?.projectLinks || {},
    };

    try {
      await onSave(updatedProject);
      setIsEditing(false);
      if (isCreating) {
        onClose();
      }
    } catch (error) {
      console.error(error);
      setErrorMessage("An error occurred while saving. Please try again.");
    } finally {
      setIsSaving(false);
    }
  };

  const handleToggleTeam = (id: string) => {
    setSelectedTeams((prev) =>
      prev.includes(id) ? prev.filter((i) => i !== id) : [...prev, id]
    );
  };

  const displayTitle = !isCreating && item ? item.name : "Create Project";

  const mappedTeams = teamsList.filter((team): team is TeamDTO & { id: string } => team.id != null).map((team) => {
    const day = getDayName(team.weeklyMeetingDay);
    const time = formatTime(team.weeklyMeetingTime);
    const studentCount = team.students?.length ?? 0;
    const mentorCount = team.mentors?.length ?? 0;

    return {
      id: team.id,
      primaryText: `Team - ${day} at ${time}`,
      secondaryText: `${studentCount} student${studentCount !== 1 ? "s" : ""} • ${mentorCount} mentor${mentorCount !== 1 ? "s" : ""}`,
    };
  });

  return (
    <Dialog open={isOpen} onOpenChange={(open: boolean) => !open && onClose()}>
      <DialogContent
        className="max-w-md w-full sm:max-w-2xl bg-secondary text-foreground border-none shadow-lg rounded-3xl p-4 sm:p-8 flex flex-col max-h-[90dvh] overflow-hidden"
        showCloseButton={false}
        onOpenAutoFocus={(e: FocusEvent) => e.preventDefault()}
      >
        <DialogHeader className="flex flex-row justify-between items-start mb-4 shrink-0">
          <div className="flex flex-col gap-1">
            <DialogTitle className="text-xl sm:text-2xl font-bold text-foreground">
              {isCreating ? "Create Project" : displayTitle}
            </DialogTitle>
            <DialogDescription className="sr-only">
              Details and editing form for Project
            </DialogDescription>
          </div>
          <Button
            variant="ghost"
            size="icon"
            onClick={onClose}
            className="hover:bg-accent/20 hover:text-accent-foreground hover:cursor-pointer rounded-full shrink-0"
          >
            <X className="w-6 h-6 text-muted-foreground" />
          </Button>
        </DialogHeader>

        {isLoading ? (
          <div className="flex-1 flex items-center justify-center text-muted-foreground py-12">
            Loading project metadata and lists...
          </div>
        ) : (
          <div className="flex-1 overflow-y-auto pl-2 -ml-2 pr-2 py-1 scrollbar-thin scrollbar-thumb-muted-foreground/20 scrollbar-track-transparent space-y-5">
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4 pt-1">
              <div className="flex flex-col gap-1 w-full sm:col-span-2">
                <div className="flex items-center gap-2 bg-background rounded-xl px-4 h-12 shadow-sm border border-transparent focus-within:ring-2 focus-within:ring-ring focus-within:ring-offset-2 transition-all w-full">
                  <span className="text-sm font-medium text-muted-foreground whitespace-nowrap">
                    Name: <span className="text-destructive">*</span>
                  </span>
                  {isEditing ? (
                    <input
                      type="text"
                      value={name}
                      onChange={(e) => setName(e.target.value)}
                      className="flex-1 bg-transparent border-none outline-none text-foreground text-sm p-0 focus:ring-0 min-w-0"
                    />
                  ) : (
                    <span className="text-sm text-foreground truncate">{name}</span>
                  )}
                </div>
              </div>

              <div className="flex flex-col gap-1 w-full sm:col-span-2">
                <div className="flex items-center gap-2 bg-background rounded-xl px-4 h-12 shadow-sm border border-transparent focus-within:ring-2 focus-within:ring-ring focus-within:ring-offset-2 transition-all w-full">
                  <span className="text-sm font-medium text-muted-foreground whitespace-nowrap">
                    Description: <span className="text-destructive">*</span>
                  </span>
                  {isEditing ? (
                    <input
                      type="text"
                      value={description}
                      onChange={(e) => setDescription(e.target.value)}
                      className="flex-1 bg-transparent border-none outline-none text-foreground text-sm p-0 focus:ring-0 min-w-0"
                    />
                  ) : (
                    <span className="text-sm text-foreground truncate">{description}</span>
                  )}
                </div>
              </div>
            </div>

            <SearchableCheckboxList
              title="Assigned Teams"
              isEditing={isEditing}
              searchValue={teamSearch}
              onSearchChange={setTeamSearch}
              items={mappedTeams}
              selectedIds={selectedTeams}
              onToggle={handleToggleTeam}
              emptyMessage="No teams found in the system."
            />
          </div>
        )}

        <div className="mt-4 pt-4 border-t border-foreground/5 flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4 shrink-0">
          <div className="flex-1 w-full">
            {errorMessage && (
              <div className="py-2 px-3 rounded-lg bg-destructive/10 border border-destructive/20 text-destructive flex items-center gap-2 animate-in fade-in slide-in-from-bottom-2">
                <AlertCircle className="w-5 h-5 shrink-0" />
                <span className="text-sm font-medium leading-tight">{errorMessage}</span>
              </div>
            )}
          </div>

          <div className="flex flex-row items-center gap-3 shrink-0 flex-nowrap sm:ml-auto w-full sm:w-auto justify-end">
            {!isEditing ? (
              <Button
                onClick={() => setIsEditing(true)}
                className="bg-primary text-primary-foreground cursor-pointer hover:bg-primary/90 rounded-xl px-6 py-6 h-auto shadow-md flex items-center gap-2 whitespace-nowrap"
              >
                <Pencil className="w-4 h-4" />
                Edit
              </Button>
            ) : (
              <>
                <Button
                  variant="ghost"
                  onClick={handleCancelEdit}
                  disabled={isSaving}
                  className="hover:bg-foreground/5 cursor-pointer text-muted-foreground rounded-xl px-6 py-6 h-auto whitespace-nowrap transition-none"
                >
                  Cancel
                </Button>
                <Button
                  onClick={handleSave}
                  disabled={isSaving}
                  className="bg-primary text-primary-foreground cursor-pointer hover:bg-primary/90 rounded-xl px-8 py-6 h-auto shadow-md flex items-center gap-2 whitespace-nowrap disabled:opacity-70 disabled:cursor-not-allowed"
                >
                  {isSaving ? "Saving..." : "Save"}
                  <Save className="w-4 h-4" />
                </Button>
              </>
            )}
          </div>
        </div>
      </DialogContent>
    </Dialog>
  );
}
