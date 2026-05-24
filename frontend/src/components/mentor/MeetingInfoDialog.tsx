import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogDescription,
  Button,
  Checkbox,
} from "@evonaplo/ui-library";
import {
  Clock,
  MapPin,
  FolderRoot,
  CheckCircle2,
  Check,
  Loader2,
  X,
  AlertCircle
} from "lucide-react";
import { useState } from "react";
import { formatTime } from "../../lib/date-utils";
import { type UIMeeting, type UIStudent } from "../../types";

interface MeetingInfoDialogProps {
  isOpen: boolean;
  onClose: () => void;
  onConfirm: (presentStudentIds: string[]) => Promise<void>;
  meeting: UIMeeting | null;
}

export default function MeetingInfoDialog({ isOpen, onClose, onConfirm, meeting }: MeetingInfoDialogProps) {
  const [attendance, setAttendance] = useState<UIStudent[]>(() =>
    meeting?.students.map((s) => ({
      id: s.id,
      name: s.name,
      isPresent: s.isPresent ?? true,
    })) || []
  );
  const [isSaving, setIsSaving] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const toggleAttendance = (id: string) => {
    setAttendance((prev) =>
      prev.map((s) => (s.id === id ? { ...s, isPresent: !s.isPresent } : s))
    );
  };

  const handleConfirm = async () => {
    setIsSaving(true);
    setErrorMessage(null);
    try {
      const presentIds = attendance
        .filter((s) => s.isPresent)
        .map((s) => s.id);
      await onConfirm(presentIds);
      onClose();
    } catch (error) {
      console.error("Failed to save attendance:", error);
      setErrorMessage("An error occurred while saving. Please try again.");
    } finally {
      setIsSaving(false);
    }
  };

  if (!meeting) return null;

  return (
    <Dialog open={isOpen} onOpenChange={(open: boolean) => !open && onClose()}>
      <DialogContent
        className="max-w-md w-full sm:max-w-2xl bg-secondary text-foreground border-none shadow-lg rounded-3xl p-4 sm:p-8 flex flex-col max-h-[90dvh] overflow-hidden"
        showCloseButton={false}
      >
        <DialogHeader className="flex flex-row justify-between items-start mb-4 shrink-0">
          <div className="flex flex-col gap-1">
            <DialogTitle className="text-xl sm:text-2xl font-bold text-foreground">
              Meeting Info
            </DialogTitle>
            <DialogDescription className="sr-only">
              View meeting details and track attendance for the selected team.
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

        <div className="flex-1 overflow-y-auto -mr-2 sm:-mr-4 pr-2 sm:pr-4 pl-1 py-2 scrollbar-thin scrollbar-thumb-muted-foreground/20 scrollbar-track-transparent space-y-5">
          <div className="space-y-4">
            <div className="flex items-center gap-3 bg-background rounded-xl p-3 px-4 shadow-sm border border-transparent">
              <Clock className="w-5 h-5 text-primary" />
              <div className="flex gap-2 text-foreground font-medium">
                <span className="font-bold">Date:</span>
                <span>{meeting.date} {formatTime(meeting.time)}</span>
              </div>
            </div>

            <div className="flex items-center gap-3 bg-background rounded-xl p-3 px-4 shadow-sm border border-transparent">
              <MapPin className="w-5 h-5 text-destructive" />
              <div className="flex gap-2 text-foreground font-medium">
                <span className="font-bold">Place:</span>
                <span>{meeting.location}</span>
              </div>
            </div>

            <div className="flex items-center gap-3 bg-background rounded-xl p-3 px-4 shadow-sm border border-transparent">
              <FolderRoot className="w-5 h-5 text-primary" />
              <div className="flex gap-2 text-foreground font-medium">
                <span className="font-bold">Project:</span>
                <span>{meeting.projectName}</span>
              </div>
            </div>
          </div>

          <div className="bg-background rounded-2xl p-6 pb-8 space-y-4 shadow-sm border border-transparent">
            <div className="flex items-center gap-2 text-foreground font-bold">
              <CheckCircle2 className="w-5 h-5 text-primary" />
              <span>Attendance:</span>
            </div>

            <div className="space-y-3">
              {attendance.map((student) => (
                <div key={student.id} className="flex items-center justify-between group gap-4">
                  <span className="text-md text-foreground/90 font-medium wrap-break-word">{student.name}</span>
                  <Checkbox
                    checked={student.isPresent}
                    onCheckedChange={() => toggleAttendance(student.id)}
                    className="w-6 h-6 rounded-md transition-colors cursor-pointer shrink-0"
                  />
                </div>
              ))}
            </div>
          </div>
        </div>

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
            <Button
              onClick={handleConfirm}
              disabled={isSaving}
              className="bg-primary text-primary-foreground cursor-pointer hover:bg-primary/90 rounded-xl px-8 py-6 h-auto shadow-md flex items-center gap-2 whitespace-nowrap disabled:opacity-70 disabled:cursor-not-allowed font-bold"
            >
              {isSaving ? (
                <Loader2 className="w-5 h-5 animate-spin" />
              ) : (
                <Check className="w-5 h-5" />
              )}
              Confirm
            </Button>
          </div>
        </div>
      </DialogContent>
    </Dialog>
  );
}
