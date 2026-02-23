export type TaskStatus = "Draft" | "InProgress" | "Completed";
export type TaskPriority = "Low" | "Medium" | "High";

const STATUS_BY_NUMBER: Record<number, TaskStatus> = {
  0: "Draft",
  1: "InProgress",
  2: "Completed",
};

const PRIORITY_BY_NUMBER: Record<number, TaskPriority> = {
  0: "Low",
  1: "Medium",
  2: "High",
};

const STATUS_LABELS: Record<TaskStatus, string> = {
  Draft: "Draft",
  InProgress: "In Progress",
  Completed: "Completed",
};

const PRIORITY_LABELS: Record<TaskPriority, string> = {
  Low: "Low",
  Medium: "Medium",
  High: "High",
};

export const normalizeStatus = function (
  value: number | string | undefined | null
): TaskStatus {
  if (value === undefined || value === null) {
    return "Draft";
  }
  if (typeof value === "number") {
    return STATUS_BY_NUMBER[value] ?? "Draft";
  }
  const s = String(value);
  if (s === "Draft" || s === "InProgress" || s === "Completed") {
    return s;
  }
  return "Draft";
};

export const normalizePriority = function (
  value: number | string | undefined | null
): TaskPriority {
  if (value === undefined || value === null) {
    return "Low";
  }
  if (typeof value === "number") {
    return PRIORITY_BY_NUMBER[value] ?? "Low";
  }
  const p = String(value);
  if (p === "Low" || p === "Medium" || p === "High") {
    return p;
  }
  return "Low";
};

export const getStatusLabel = function (
  value: number | string | undefined | null
): string {
  return STATUS_LABELS[normalizeStatus(value)];
};

export const getPriorityLabel = function (
  value: number | string | undefined | null
): string {
  return PRIORITY_LABELS[normalizePriority(value)];
};

export interface Task {
  id: number;
  title: string;
  description: string;
  status: TaskStatus | number;
  priority: TaskPriority | number;
  userId: number;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
}

export interface CreateTaskRequest {
  title: string;
  description: string;
  priority?: TaskPriority;
  status?: TaskStatus;
  assigneeUserId?: number;
}

export interface UpdateTaskRequest {
  title?: string;
  description?: string;
  priority?: TaskPriority;
  status?: TaskStatus;
}

export interface GetTasksParams {
  pageNumber?: number;
  pageSize?: number;
  status?: TaskStatus | "";
}
