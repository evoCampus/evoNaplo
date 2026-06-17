import { createContext } from "react";
import ApiClient from "../api/api.client";

export const ApiClientContext = createContext<ApiClient | null>(null);
