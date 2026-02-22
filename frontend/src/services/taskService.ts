import type { CreateTaskRequest, UpdateTaskRequest } from "../types/task";
import { api } from "./api";

const BASE = "/tasks";

export const getTasks = async function (params = {}) {
  const { data } = await api.get(BASE, { params });
  return data;
};

export const createTask = async function (dto: CreateTaskRequest) {
  const { data } = await api.post(BASE, dto);
  return data;
};

export const updateTask = async function (id: number, dto: UpdateTaskRequest) {
  const { data } = await api.put(`${BASE}/${id}`, dto);
  return data;
};

export const deleteTask = async function (id: number) {
  await api.delete(`${BASE}/${id}`);
};
