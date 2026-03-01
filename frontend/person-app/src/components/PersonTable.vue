<script setup lang="ts">
import { ref } from "vue";
import { personService } from "../services/personService";
import type { Person } from "../types/person";
import AddPersonModal from "./AddPersonModal.vue";
import ViewPersonModal from "./ViewPersonModal.vue";

const persons = ref<Person[]>([]);
const totalCount = ref(0);
const loading = ref(false);
const error = ref("");

const showAddModal = ref(false);
const showViewModal = ref(false);
const selectedPerson = ref<Person | null>(null);

// เก็บไว้คำนวณเลขลำดับ + reload หลัง ADD
const currentPage = ref(1);
const currentPageSize = ref(10);

const headers = [
  { title: "ลำดับ", key: "id", sortable: false, width: "80px" },
  { title: "ชื่อ-สกุล", key: "fullName", sortable: false },
  { title: "ที่อยู่", key: "address", sortable: false },
  { title: "วันเกิด", key: "dateOfBirth", sortable: false },
  { title: "อายุ", key: "age", sortable: false, width: "80px" },
  { title: "Action", key: "action", sortable: false, width: "100px" },
];

async function loadPersons({
  page,
  itemsPerPage,
}: {
  page: number;
  itemsPerPage: number;
}) {
  currentPage.value = page;
  currentPageSize.value = itemsPerPage;
  loading.value = true;
  error.value = "";
  try {
    const result = await personService.getPaged(page, itemsPerPage);
    persons.value = result.items;
    totalCount.value = result.totalCount;
  } catch (e) {
    error.value = "ไม่สามารถโหลดข้อมูลได้";
    console.error(e);
  } finally {
    loading.value = false;
  }
}

function handleView(person: Person) {
  selectedPerson.value = person;
  showViewModal.value = true;
}

function handleSaved() {
  loadPersons({ page: currentPage.value, itemsPerPage: currentPageSize.value });
}
</script>

<template>
  <v-container>
    <v-row align="center" class="mb-4">
      <v-col>
        <h2 class="text-h6 font-weight-bold text-blue-darken-3">
          IT 01-1 — รายการบุคคล
        </h2>
      </v-col>
      <v-col cols="auto">
        <v-btn
          color="teal-darken-1"
          variant="elevated"
          prepend-icon="mdi-plus"
          @click="showAddModal = true"
        >
          ADD
        </v-btn>
      </v-col>
    </v-row>

    <v-alert v-if="error" type="error" class="mb-4" rounded="lg">{{
      error
    }}</v-alert>

    <v-card elevation="6" rounded="xl">
      <v-card-title
        class="bg-blue-darken-2 text-white pa-4 text-subtitle-1 font-weight-bold"
      >
        รายการบุคคลทั้งหมด
      </v-card-title>
      <v-data-table-server
        :items="persons"
        :items-length="totalCount"
        :headers="headers"
        :loading="loading"
        :items-per-page="10"
        :items-per-page-options="[10, 25, 50]"
        no-data-text="ไม่มีข้อมูล"
        hover
        @update:options="loadPersons"
      >
        <template #item.id="{ index }">
          <span class="text-grey-darken-1 font-weight-medium">
            {{ (currentPage - 1) * currentPageSize + index + 1 }}
          </span>
        </template>

        <template #item.fullName="{ item }">
          <span class="font-weight-medium"
            >{{ item.firstName }} {{ item.lastName }}</span
          >
        </template>

        <template #item.dateOfBirth="{ item }">
          {{
            new Date(item.dateOfBirth).toLocaleDateString("th-TH", {
              year: "numeric",
              month: "2-digit",
              day: "2-digit",
            })
          }}
        </template>

        <template #item.action="{ item }">
          <v-btn
            color="blue-darken-1"
            size="small"
            variant="tonal"
            @click="handleView(item)"
          >
            View
          </v-btn>
        </template>

        <template #loading>
          <div class="text-center py-10">
            <v-progress-circular
              indeterminate
              color="blue-darken-2"
              size="64"
              width="6"
            />
            <div class="mt-4 text-blue-darken-2 font-weight-bold text-h6">
              กำลังโหลดข้อมูล...
            </div>
          </div>
        </template>
      </v-data-table-server>
    </v-card>

    <AddPersonModal
      :show="showAddModal"
      @close="showAddModal = false"
      @saved="handleSaved"
    />
    <ViewPersonModal
      :show="showViewModal"
      :person="selectedPerson"
      @close="showViewModal = false"
    />
  </v-container>
</template>
