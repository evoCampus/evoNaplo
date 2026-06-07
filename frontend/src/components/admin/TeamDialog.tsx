import React, { useState, useEffect } from "react";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  Button,
  Select,
  SelectTrigger,
  SelectValue,
  SelectContent,
  SelectItem,
} from "@evonaplo/ui-library";
import { Pencil, Save, X, AlertCircle } from "lucide-react";
import { useApiClient } from "../../hooks/use-api-client";
import type { TeamDTO, StudentDTO, MentorDTO, ProjectDTO, DayOfWeek } from "../../api";
import { SearchableCheckboxList } from "./SearchableCheckboxList";
import { getDayName, DAYS_OF_WEEK } from "../../lib/date-utils";

interface TeamDialogProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (team: TeamDTO, selectedProjectIds: string[]) => Promise<void>;
  item: TeamDTO | null;
  isCreating?: boolean;
}

export function TeamDialog({
  isOpen,
  onClose,
  onSave,
  item,
  isCreating = false,
}: TeamDialogProps) {
  const apiClient = useApiClient();

  const [prevIsOpen, setPrevIsOpen] = useState(isOpen);
  const [prevItem, setPrevItem] = useState(item);

  const [isEditing, setIsEditing] = useState(isCreating);
  const [weeklyMeetingDay, setWeeklyMeetingDay] = useState<DayOfWeek>(item?.weeklyMeetingDay ?? 0);
  const [weeklyMeetingTime, setWeeklyMeetingTime] = useState<string>(item?.weeklyMeetingTime ?? "12:00");
  const [selectedStudents, setSelectedStudents] = useState<string[]>(item?.students || []);
  const [selectedMentors, setSelectedMentors] = useState<string[]>(item?.mentors || []);
  const [selectedProjects, setSelectedProjects] = useState<string[]>([]);

  const [studentSearch, setStudentSearch] = useState("");
  const [mentorSearch, setMentorSearch] = useState("");
  const [projectSearch, setProjectSearch] = useState("");

  const [studentsList, setStudentsList] = useState<StudentDTO[]>([]);
  const [mentorsList, setMentorsList] = useState<MentorDTO[]>([]);
  const [projectsList, setProjectsList] = useState<ProjectDTO[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [isSaving, setIsSaving] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  if (isOpen !== prevIsOpen || item !== prevItem) {
    setPrevIsOpen(isOpen);
    setPrevItem(item);

    if (isOpen) {
      setIsEditing(isCreating);
      setErrorMessage(null);
      setStudentSearch("");
      setMentorSearch("");
      setProjectSearch("");

      if (item) {
        setWeeklyMeetingDay(item.weeklyMeetingDay ?? 1);
        setWeeklyMeetingTime(item.weeklyMeetingTime ?? "12:00");
        setSelectedStudents(item.students || []);
        setSelectedMentors(item.mentors || []);
      } else {
        setWeeklyMeetingDay(1);
        setWeeklyMeetingTime("12:00");
        setSelectedStudents([]);
        setSelectedMentors([]);
        setSelectedProjects([]);
      }
    }
  }

  useEffect(() => {
    if (isOpen) {
      const fetchData = async () => {
        setIsLoading(true);
        try {
          const [studentsRes, mentorsRes, projectsRes] = await Promise.all([
            apiClient.students.apiStudentsGet(),
            apiClient.mentors.apiMentorsGet(),
            apiClient.projects.apiProjectsGet(),
          ]);

          setStudentsList(studentsRes.data);
          setMentorsList(mentorsRes.data);
          setProjectsList(projectsRes.data);

          if (item?.id) {
            const teamProjects = projectsRes.data
              .filter((p) => p.teams?.includes(item.id!))
              .map((p) => p.id!);
            setSelectedProjects(teamProjects);
          } else {
            setSelectedProjects([]);
          }
        } catch (error) {
          console.error("Failed to load team references:", error);
          setErrorMessage("Failed to load options from the system.");
        } finally {
          setIsLoading(false);
        }
      };

      fetchData();
    }
  }, [isOpen, item, apiClient]);

  const handleCancelEdit = () => {
    setErrorMessage(null);
    if (isCreating) {
      onClose();
    } else {
      setIsEditing(false);
      if (item) {
        setWeeklyMeetingDay(item.weeklyMeetingDay ?? 0);
        setWeeklyMeetingTime(item.weeklyMeetingTime ?? "12:00");
        setSelectedStudents(item.students || []);
        setSelectedMentors(item.mentors || []);
        const teamProjects = projectsList
          .filter((p) => p.teams?.includes(item.id!))
          .map((p) => p.id!);
        setSelectedProjects(teamProjects);
      }
    }
  };

  const handleSave = async () => {
    setIsSaving(true);
    setErrorMessage(null);

    if (weeklyMeetingDay < 0 || weeklyMeetingDay > 6) {
      setErrorMessage("Meeting day must be between 0 (Monday) and 6 (Sunday).");
      setIsSaving(false);
      return;
    }

    const timeRegex = /^([01]\d|2[0-3]):([0-5]\d)$/;
    if (!weeklyMeetingTime.trim() || !timeRegex.test(weeklyMeetingTime)) {
      setErrorMessage("Meeting time must be a valid time in HH:MM format (e.g. 14:30).");
      setIsSaving(false);
      return;
    }

    const updatedTeam: TeamDTO = {
      ...item,
      id: item?.id ?? null,
      weeklyMeetingDay,
      weeklyMeetingTime,
      mentors: selectedMentors,
      students: selectedStudents,
      attendance: item?.attendance || [],
    };

    try {
      await onSave(updatedTeam, selectedProjects);
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

  const handleToggleId = (
    id: string,
    selectedList: string[],
    setList: React.Dispatch<React.SetStateAction<string[]>>
  ) => {
    setList((prev) =>
      selectedList.includes(id) ? prev.filter((i) => i !== id) : [...prev, id]
    );
  };

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
              {isCreating ? "Create Team" : "Team Details"}
            </DialogTitle>
            <DialogDescription className="sr-only">
              Details and editing form for Team
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
            Loading team metadata and lists...
          </div>
        ) : (
          <div className="flex-1 overflow-y-auto pl-2 -ml-2 pr-2 py-1 scrollbar-thin scrollbar-thumb-muted-foreground/20 scrollbar-track-transparent space-y-5">
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4 pt-1">
              <div className="flex flex-col gap-1 w-full">
                <div className="flex items-center gap-2 bg-background rounded-xl pl-4 pr-2 h-12 shadow-sm border border-transparent focus-within:ring-2 focus-within:ring-ring focus-within:ring-offset-2 transition-all w-full">
                  <span className="text-sm font-medium text-muted-foreground whitespace-nowrap/80 shrink-0">
                    Meeting Day: <span className="text-destructive">*</span>
                  </span>
                  {isEditing ? (
                    <Select
                      value={String(weeklyMeetingDay)}
                      onValueChange={(val: string) => setWeeklyMeetingDay(Number(val) as DayOfWeek)}
                    >
                      <SelectTrigger className="flex-1 border-none bg-transparent hover:bg-foreground/5 h-9 min-w-0 text-sm text-foreground shadow-none focus:ring-0 focus-visible:ring-0 focus-visible:ring-offset-0 outline-none w-full pl-3 pr-2 rounded-lg">
                        <SelectValue />
                      </SelectTrigger>
                      <SelectContent>
                        {DAYS_OF_WEEK.map((name, index) => (
                          <SelectItem key={index} value={String(index)}>{name}</SelectItem>
                        ))}
                      </SelectContent>
                    </Select>
                  ) : (
                    <span className="text-sm text-foreground pl-2">
                      {getDayName(weeklyMeetingDay)}
                    </span>
                  )}
                </div>
              </div>

              <div className="flex flex-col gap-1 w-full">
                <div className="flex items-center gap-2 bg-background rounded-xl px-4 h-12 shadow-sm border border-transparent focus-within:ring-2 focus-within:ring-ring focus-within:ring-offset-2 transition-all w-full">
                  <span className="text-sm font-medium text-muted-foreground whitespace-nowrap">
                    Meeting Time: <span className="text-destructive">*</span>
                  </span>
                  {isEditing ? (
                    <input
                      type="time"
                      value={weeklyMeetingTime}
                      onChange={(e) => setWeeklyMeetingTime(e.target.value)}
                      className="flex-1 bg-transparent border-none outline-none text-foreground text-sm p-0 focus:ring-0 min-w-0 cursor-pointer"
                    />
                  ) : (
                    <span className="text-sm text-foreground">{weeklyMeetingTime}</span>
                  )}
                </div>
              </div>
            </div>

            <SearchableCheckboxList
              title="Assigned Projects"
              isEditing={isEditing}
              searchValue={projectSearch}
              onSearchChange={setProjectSearch}
              items={projectsList.map((p) => ({
                id: p.id!,
                primaryText: p.name || "",
                secondaryText: p.description || undefined,
              }))}
              selectedIds={selectedProjects}
              onToggle={(id) => handleToggleId(id, selectedProjects, setSelectedProjects)}
              emptyMessage="No projects found."
            />

            <SearchableCheckboxList
              title="Assigned Mentors"
              isEditing={isEditing}
              searchValue={mentorSearch}
              onSearchChange={setMentorSearch}
              items={mentorsList.map((m) => ({
                id: m.id!,
                primaryText: m.name || "",
                secondaryText: m.email || undefined,
              }))}
              selectedIds={selectedMentors}
              onToggle={(id) => handleToggleId(id, selectedMentors, setSelectedMentors)}
              emptyMessage="No mentors found."
            />

            <SearchableCheckboxList
              title="Assigned Students"
              isEditing={isEditing}
              searchValue={studentSearch}
              onSearchChange={setStudentSearch}
              items={studentsList.map((s) => ({
                id: s.id!,
                primaryText: s.name || "",
                secondaryText: s.email || undefined,
              }))}
              selectedIds={selectedStudents}
              onToggle={(id) => handleToggleId(id, selectedStudents, setSelectedStudents)}
              emptyMessage="No students found."
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
