import { useEffect, useRef, useState } from "react";
import { Trash2, Loader2, AlertCircle, ChevronLeft, ChevronRight } from "lucide-react";
import type { ReactNode } from "react";
import { Button } from "@evonaplo/ui-library";

const DEFAULT_PAGE_SIZE = 10;

interface GenericEntityListProps<T extends { id?: string | null }> {
  dataPromise: Promise<T[]>;
  onEdit: (item: T) => void;
  onDelete: (id: string) => void;
  renderContent: (item: T) => ReactNode;
  emptyMessage?: string;
  pageSize?: number;
}

export function GenericEntityList<T extends { id?: string | null }>({
  dataPromise,
  onEdit,
  onDelete,
  renderContent,
  emptyMessage = "No items found.",
  pageSize = DEFAULT_PAGE_SIZE,
}: GenericEntityListProps<T>) {
  const [items, setItems] = useState<T[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [page, setPage] = useState(0);
  const hasLoadedOnce = useRef(false);

  useEffect(() => {
    let cancelled = false;

    dataPromise
      .then(result => {
        if (!cancelled) {
          setItems(result);
          setPage(0);
          setError(null);
          hasLoadedOnce.current = true;
          setIsLoading(false);
        }
      })
      .catch(err => {
        if (!cancelled) {
          console.error("Failed to load data:", err);
          setError("Failed to load data. Please try again.");
          setIsLoading(false);
        }
      });

    return () => {
      cancelled = true;
    };
  }, [dataPromise]);

  if (isLoading) {
    return (
      <div className="flex flex-col items-center justify-center py-20 text-muted-foreground gap-3">
        <Loader2 className="w-8 h-8 animate-spin text-primary" />
        <span className="text-sm font-medium">Loading...</span>
      </div>
    );
  }

  if (error) {
    return (
      <div className="flex flex-col items-center justify-center py-12 gap-3">
        <AlertCircle className="w-8 h-8 text-destructive" />
        <span className="text-sm font-medium text-destructive">{error}</span>
      </div>
    );
  }

  const totalPages = Math.ceil(items.length / pageSize);
  const pageItems = items.slice(page * pageSize, (page + 1) * pageSize);

  return (
    <div className="grid gap-3">
      {pageItems.map((item, index) => (
        <div
          key={item.id ?? `item-${index}`}
          className="flex items-center justify-between p-5 bg-card rounded-2xl border border-transparent hover:border-border/50 transition-all group shadow-sm hover:shadow-md cursor-pointer"
          onClick={() => onEdit(item)}
        >
          <div className="flex-1 min-w-0">{renderContent(item)}</div>
          <Button
            variant="ghost"
            size="icon"
            onClick={(e: React.MouseEvent<HTMLButtonElement>) => {
              e.stopPropagation();
              if (!item.id) return;
              onDelete(item.id);
            }}
            className="text-muted-foreground hover:text-destructive hover:bg-destructive/10 ml-4 shrink-0 rounded-full cursor-pointer"
          >
            <Trash2 className="w-5 h-5" />
          </Button>
        </div>
      ))}

      {items.length === 0 && (
        <div className="text-center p-8 text-muted-foreground">{emptyMessage}</div>
      )}

      {totalPages > 1 && (
        <div className="flex items-center justify-center gap-4 pt-4">
          <Button
            variant="ghost"
            size="icon"
            onClick={() => setPage(p => Math.max(0, p - 1))}
            disabled={page === 0}
            aria-label="Previous page"
          >
            <ChevronLeft className="w-5 h-5" />
          </Button>
          <span className="text-sm text-muted-foreground font-medium">
            {page + 1} / {totalPages}
          </span>
          <Button
            variant="ghost"
            size="icon"
            onClick={() => setPage(p => Math.min(totalPages - 1, p + 1))}
            disabled={page === totalPages - 1}
            aria-label="Next page"
          >
            <ChevronRight className="w-5 h-5" />
          </Button>
        </div>
      )}
    </div>
  );
}
