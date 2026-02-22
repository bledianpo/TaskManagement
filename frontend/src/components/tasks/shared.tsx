import { useMemo } from "react";
import { createListCollection, Field, Select } from "@chakra-ui/react";

export const FormSelect = function ({
  label,
  value,
  options,
  onChange,
}: {
  label: string;
  value: string;
  options: { value: string; label: string }[];
  onChange: (value: string) => void;
}) {
  const collection = useMemo(
    () =>
      createListCollection<{ value: string; label: string }>({
        items: options,
        itemToString: (item) => item.label,
        itemToValue: (item) => item.value,
      }),
    [options]
  );

  return (
    <Field.Root mt={3}>
      <Field.Label>{label}</Field.Label>
      <Select.Root
        collection={collection}
        value={value ? [value] : []}
        onValueChange={(e) => onChange(e.value[0] ?? "")}
      >
        <Select.HiddenSelect />
        <Select.Control>
          <Select.Trigger>
            <Select.ValueText placeholder="Select..." />
          </Select.Trigger>
          <Select.IndicatorGroup>
            <Select.Indicator />
          </Select.IndicatorGroup>
        </Select.Control>
        <Select.Positioner>
          <Select.Content>
            {options.map((o) => (
              <Select.Item key={o.value} item={o}>
                <Select.ItemText>{o.label}</Select.ItemText>
              </Select.Item>
            ))}
          </Select.Content>
        </Select.Positioner>
      </Select.Root>
    </Field.Root>
  );
};

export const PRIORITY_OPTIONS = [
  { value: "Low", label: "Low" },
  { value: "Medium", label: "Medium" },
  { value: "High", label: "High" },
];

export const STATUS_OPTIONS = [
  { value: "Draft", label: "Draft" },
  { value: "InProgress", label: "In Progress" },
  { value: "Completed", label: "Completed" },
];
