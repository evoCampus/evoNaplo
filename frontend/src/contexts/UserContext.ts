import { createContext } from "react";
import type { User } from "../types";

interface UserContextType {
  user: User | null;
  setUser: (user: User | null) => void;
  isLoading: boolean;
}

export const UserContext = createContext<UserContextType | undefined>(undefined);
