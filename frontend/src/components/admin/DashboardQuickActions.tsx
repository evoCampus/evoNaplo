import { Plus, ArrowRight } from "lucide-react";
import type { LucideIcon } from "lucide-react";

export interface QuickAction {
  title: string;
  description: string;
  icon: LucideIcon;
  onClick: () => void;
  isAdd?: boolean;
}

interface DashboardQuickActionsProps {
  filteredActions: QuickAction[];
}

export default function DashboardQuickActions({ filteredActions }: DashboardQuickActionsProps) {
  return (
    <div className="space-y-6">
      <h2 className="text-xl font-semibold tracking-tight text-foreground/90">Quick Actions</h2>
      {filteredActions.length > 0 ? (
        <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 gap-5">
          {filteredActions.map((action) => {
            const ActionIcon = action.icon;
            return (
              <div
                key={action.title}
                onClick={action.onClick}
                className="flex flex-col justify-between p-6 bg-card rounded-2xl border border-transparent hover:border-border/50 shadow-sm hover:shadow-md hover:-translate-y-0.5 transition-all duration-200 cursor-pointer group h-44"
              >
                <div className="flex justify-between items-start">
                  <div className="p-3 bg-primary/10 rounded-xl text-primary group-hover:scale-105 group-hover:bg-primary/15 transition-all duration-200">
                    <ActionIcon className="h-6 w-6" />
                  </div>
                  <div className="w-8 h-8 rounded-full bg-muted/50 flex items-center justify-center text-muted-foreground group-hover:text-primary group-hover:bg-primary/10 transition-colors duration-200">
                    {action.isAdd ? (
                      <Plus className="h-4 w-4" />
                    ) : (
                      <ArrowRight className="h-4 w-4" />
                    )}
                  </div>
                </div>
                <div className="space-y-1">
                  <h3 className="text-lg font-semibold text-foreground/90 group-hover:text-primary transition-colors">
                    {action.title}
                  </h3>
                  <p className="text-sm text-muted-foreground leading-normal line-clamp-2">
                    {action.description}
                  </p>
                </div>
              </div>
            );
          })}
        </div>
      ) : (
        <div className="text-center p-12 bg-card rounded-2xl border border-dashed border-border/60 text-muted-foreground">
          No matching actions found. Try another search!
        </div>
      )}
    </div>
  );
}
