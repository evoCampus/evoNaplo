import { createContext } from "react";

interface SemesterContextType {
  currentSemester: number;
  setCurrentSemester: (semester: number) => void;
  availableSemesters: number[];
}

export const SemesterContext = createContext<SemesterContextType | undefined>(undefined);
