import { useContext } from "react";
import { SemesterContext } from "../contexts/SemesterContext";

export function useSemester() {
  const context = useContext(SemesterContext);

  if (!context) {
    throw new Error("useSemester must be used within a SemesterProvider");
  }
  return context;
}
