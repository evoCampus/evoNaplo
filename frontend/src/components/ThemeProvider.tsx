import { useEffect, useState } from "react";
import { type Theme, ThemeProviderContext } from "../contexts/ThemeContext";

interface ThemeProviderProps {
  children: React.ReactNode;
}

const THEME_CONFIG = {
  defaultTheme: "light" as Theme,
  storageKey: "evo-naplo-theme",
};

export function ThemeProvider({
  children,
  ...props
}: ThemeProviderProps) {
  const [theme, setTheme] = useState<Theme>(
    () => (localStorage.getItem(THEME_CONFIG.storageKey) as Theme) || THEME_CONFIG.defaultTheme
  );

  useEffect(() => {
    const root = window.document.documentElement;

    root.classList.remove("light", "dark");

    if (theme === "system") {
      const systemTheme = window.matchMedia("(prefers-color-scheme: dark)")
        .matches
        ? "dark"
        : "light";

      root.classList.add(systemTheme);
      return;
    }

    root.classList.add(theme);
  }, [theme]);

  const value = {
    theme,
    setTheme: (theme: Theme) => {
      localStorage.setItem(THEME_CONFIG.storageKey, theme);
      setTheme(theme);
    },
  };

  return (
    <ThemeProviderContext.Provider {...props} value={value}>
      {children}
    </ThemeProviderContext.Provider>
  );
}
