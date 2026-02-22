import { useState } from "react";
import {
  DialogRoot,
  DialogBackdrop,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogBody,
  DialogFooter,
  Button,
  Input,
  Field,
  Text
} from "@chakra-ui/react";
import { useMutation } from "@tanstack/react-query";
import { FormSelect, PRIORITY_OPTIONS, STATUS_OPTIONS } from "./shared";
import type {
  CreateTaskRequest,
  TaskPriority,
  TaskStatus,
} from "../../types/task";
import { createTask } from "../../services/taskService";

interface CreateTaskModalProps {
  open: boolean;
  onClose: () => void;
  onSuccess: () => void;
}

const CreateTaskModal = function ({
  open,
  onClose,
  onSuccess,
}: CreateTaskModalProps) {
  const [title, setTitle] = useState<string>("");
  const [description, setDescription] = useState<string>("");
  const [priority, setPriority] = useState<TaskPriority>("Low");
  const [status, setStatus] = useState<TaskStatus>("Draft");
  const [error, setError] = useState<string>("");

  const createMutation = useMutation({
    mutationFn: (data: CreateTaskRequest) => createTask(data),
    onSuccess: () => {
      setTitle("");
      setDescription("");
      setPriority("Low");
      setStatus("Draft");
      setError("");
      onSuccess();
      onClose();
    },
    onError: (err) => setError(err?.message || "Failed to create Task."),
  });

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!title.trim()) {
      return setError("Title is required");
    }
    if (!description.trim()) {
      return setError("Description is required");
    }
    createMutation.mutate({
      title: title.trim(),
      description: description.trim(),
      priority,
      status,
    });
  };

  return (
    <DialogRoot open={open} onOpenChange={(e) => !e.open && onClose()}>
      <DialogBackdrop />
      <DialogContent
        position="fixed"
        top="50%"
        left="50%"
        transform="translate(-50%, -50%)"
        margin="0"
        maxH="90vh"
        overflowY="auto"
      >
        <DialogHeader>
          <DialogTitle>New task</DialogTitle>
        </DialogHeader>
        <DialogBody>
          <form id="create-task-form" onSubmit={handleSubmit}>
            <Field.Root>
              <Field.Label>Title</Field.Label>
              <Input
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                placeholder="Task title"
              />
            </Field.Root>
            <Field.Root mt={3}>
              <Field.Label>Description</Field.Label>
              <Input
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                placeholder="Description"
              />
            </Field.Root>
            <FormSelect
              label="Priority"
              value={priority}
              options={PRIORITY_OPTIONS}
              onChange={(v) => setPriority(v as TaskPriority)}
            />
            <FormSelect
              label="Status"
              value={status}
              options={STATUS_OPTIONS}
              onChange={(v) => setStatus(v as TaskStatus)}
            />
          </form>
          {error && (
            <Text color="red.500" fontSize="sm" mt={2}>
              {error}
            </Text>
          )}
        </DialogBody>
        <DialogFooter>
          <Button variant="outline" onClick={onClose}>
            Cancel
          </Button>
          <Button
            type="submit"
            form="create-task-form"
            colorPalette="blue"
            loading={createMutation.isPending}
          >
            Create
          </Button>
        </DialogFooter>
      </DialogContent>
    </DialogRoot>
  );
};

export default CreateTaskModal;
