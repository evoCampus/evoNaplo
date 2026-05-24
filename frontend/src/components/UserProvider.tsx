import { useState, type ReactNode } from "react";
import { UserContext } from "../contexts/UserContext";
import type { User } from "../types";

export function UserProvider({ children }: { children: ReactNode }) {
  const [user, setUser] = useState<User | null>({
    id: "1",
    name: "Teszt Mentor",
    role: "mentor",
    email: "mentor@test.com"
  });

  const [isLoading] = useState(false);

  return (
    <UserContext.Provider value={{ user, setUser, isLoading }}>
      {children}
    </UserContext.Provider>
  );
}
