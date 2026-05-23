import { useState, useEffect, type ReactNode } from "react";
import { SemesterContext } from "../contexts/SemesterContext";

export function SemesterProvider({ children }: { children: ReactNode }) {
  const [currentSemester, setCurrentSemester] = useState<number>(() => {
    const saved = localStorage.getItem("currentSemester");
    return saved ? parseInt(saved, 10) : 20261;
  });

  const availableSemesters = [20241, 20242, 20251, 20252, 20261, 20262];

  useEffect(() => {
    localStorage.setItem("currentSemester", currentSemester.toString());
  }, [currentSemester]);

  return (
    <SemesterContext.Provider value={{ currentSemester, setCurrentSemester, availableSemesters }}>
      {children}
    </SemesterContext.Provider>
  );
}
