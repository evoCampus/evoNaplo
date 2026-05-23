import { useMemo } from "react";
import ApiClient from "../api/api.client";
import { ApiClientContext } from "../contexts/ApiClientContext";

interface ApiClientProviderProps {
  children: React.ReactNode;
  baseUrl?: string;
}

export function ApiClientProvider({ children, baseUrl }: ApiClientProviderProps) {
  const client = useMemo(() => new ApiClient(baseUrl), [baseUrl]);

  return (
    <ApiClientContext.Provider value={client}>
      {children}
    </ApiClientContext.Provider>
  );
}
