import { useContext } from "react";
import { SemesterContext } from "../components/SemesterProvider";

export function useSemester() {
  const context = useContext(SemesterContext);

  if (!context) {
    throw new Error("useSemester must be used within a SemesterProvider");
  }
  return context;
}
