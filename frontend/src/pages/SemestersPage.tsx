import { useSemester } from "../hooks/use-semester";
import { Button } from "@evonaplo/ui-library";

export default function SemestersPage() {
  const { availableSemesters, currentSemester, setCurrentSemester } = useSemester();

  return (
    <div className="space-y-6">
      <div className="flex items-center justify-between">
        <h1 className="text-3xl font-bold tracking-tight">Semesters</h1>
      </div>
      <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
        {availableSemesters.map((semester) => (
          <div
            key={semester}
            className={`p-6 rounded-lg border shadow-sm transition-colors ${
              currentSemester === semester
                ? "bg-primary/10 border-primary"
                : "bg-card hover:bg-accent"
            }`}
          >
            <div className="flex items-center justify-between">
              <div>
                <p className="text-lg font-semibold">{semester}</p>
                <p className="text-sm text-muted-foreground">
                  {semester.toString().slice(0, 4)} Semester {semester.toString().slice(4)}
                </p>
              </div>
              {currentSemester === semester ? (
                <span className="px-2 py-1 text-xs font-medium bg-primary text-primary-foreground rounded-full">
                  Current
                </span>
              ) : (
                <Button variant="outline" size="sm" onClick={() => setCurrentSemester(semester)}>
                  Select
                </Button>
              )}
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
