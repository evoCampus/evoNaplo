// import { useContext } from "react";
// import { UserContext } from "../contexts/UserContext";

export const useUser = () => {
  // Eredeti logikát kikommentezzük a fejlesztés idejére:
  // const context = useContext(UserContext);
  // if (!context) {
  //   throw new Error("useUser must be used within a UserProvider");
  // }
  // return context;

  // Helyette fixen visszaadjuk a kamu admint:
  return {
    user: { name: "test", role: "admin" },
    isLoading: false
  } as any;
};
