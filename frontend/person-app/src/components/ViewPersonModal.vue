<script setup lang="ts">
import type { Person } from "../types/person";

const props = defineProps<{
  show: boolean;
  person: Person | null;
}>();

const emit = defineEmits<{
  close: [];
}>();

// "1990-05-15" → "15/05/2533"
function formatDate(dateStr: string): string {
  if (!dateStr) return "-";
  return new Date(dateStr).toLocaleDateString("th-TH", {
    year: "numeric",
    month: "2-digit",
    day: "2-digit",
  });
}
</script>

<template>
  <v-dialog
    :modelValue="show"
    @update:modelValue="emit('close')"
    max-width="520"
    persistent
  >
    <v-card v-if="person" rounded="xl" elevation="8">
      <v-card-title
        class="bg-blue-darken-2 text-white pa-4 text-subtitle-1 font-weight-bold"
      >
        IT 01-3 — ดูข้อมูลบุคคล
      </v-card-title>

      <v-card-text class="pa-5">
        <v-row>
          <v-col cols="6">
            <div
              class="text-caption text-grey-darken-1 font-weight-medium mb-1"
            >
              ชื่อ
            </div>
            <div class="text-body-1 font-weight-medium">
              {{ person.firstName }}
            </div>
          </v-col>

          <v-col cols="6">
            <div
              class="text-caption text-grey-darken-1 font-weight-medium mb-1"
            >
              นามสกุล
            </div>
            <div class="text-body-1 font-weight-medium">
              {{ person.lastName }}
            </div>
          </v-col>

          <v-col cols="12"><v-divider /></v-col>

          <v-col cols="6">
            <div
              class="text-caption text-grey-darken-1 font-weight-medium mb-1"
            >
              วันเกิด
            </div>
            <div class="text-body-1 font-weight-medium">
              {{ formatDate(person.dateOfBirth) }}
            </div>
          </v-col>

          <v-col cols="6">
            <div
              class="text-caption text-grey-darken-1 font-weight-medium mb-1"
            >
              อายุ
            </div>
            <div class="text-body-1 font-weight-medium">
              <v-chip color="teal-darken-1" size="small" variant="tonal"
                >{{ person.age }} ปี</v-chip
              >
            </div>
          </v-col>

          <v-col cols="12"><v-divider /></v-col>

          <v-col cols="12">
            <div
              class="text-caption text-grey-darken-1 font-weight-medium mb-1"
            >
              ที่อยู่
            </div>
            <div class="text-body-1">{{ person.address || "-" }}</div>
          </v-col>
        </v-row>
      </v-card-text>

      <v-divider />

      <v-card-actions class="pa-4">
        <v-spacer />
        <v-btn color="blue-darken-1" variant="tonal" @click="emit('close')"
          >ปิด</v-btn
        >
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
