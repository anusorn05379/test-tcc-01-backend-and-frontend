<script setup lang="ts">
import { ref, computed, watch } from "vue";
import { personService } from "../services/personService";
import type { CreatePersonRequest } from "../types/person";

const props = defineProps<{
  show: boolean;
}>();

const emit = defineEmits<{
  close: [];
  saved: [];
}>();

const formRef = ref();
const saving = ref(false);

const form = ref<CreatePersonRequest>({
  firstName: "",
  lastName: "",
  dateOfBirth: "",
  address: "",
});

const age = computed(() => {
  if (!form.value.dateOfBirth) return 0;

  const today = new Date();
  const birth = new Date(form.value.dateOfBirth);
  let age = today.getFullYear() - birth.getFullYear();

  const monthDiff = today.getMonth() - birth.getMonth();
  if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birth.getDate())) {
    age--;
  }

  return age;
});

const required = (value: string) => !!value || "กรุณากรอกข้อมูล";

// reset form ทุกครั้งที่ modal เปิดขึ้น
watch(
  () => props.show,
  (newVal) => {
    if (newVal === true) {
      form.value = {
        firstName: "",
        lastName: "",
        dateOfBirth: "",
        address: "",
      };
    }
  },
);

async function handleSave() {
  const { valid } = await formRef.value.validate();
  if (!valid) return;

  saving.value = true;
  try {
    await personService.create(form.value);
    emit("saved");
    emit("close");
  } catch (e) {
    console.error("บันทึกไม่สำเร็จ:", e);
    alert("เกิดข้อผิดพลาด กรุณาลองใหม่");
  } finally {
    saving.value = false;
  }
}
</script>

<template>
  <v-dialog
    :modelValue="show"
    @update:modelValue="emit('close')"
    max-width="520"
    persistent
  >
    <v-card rounded="xl" elevation="8">
      <v-card-title
        class="bg-blue-darken-2 text-white pa-4 text-subtitle-1 font-weight-bold"
      >
        IT 01-2 — เพิ่มข้อมูลบุคคล
      </v-card-title>

      <v-card-text class="pa-5">
        <v-form ref="formRef">
          <v-row>
            <v-col cols="6">
              <v-text-field
                v-model="form.firstName"
                label="ชื่อ"
                :rules="[required]"
                variant="outlined"
                density="comfortable"
                color="blue-darken-2"
              />
            </v-col>

            <v-col cols="6">
              <v-text-field
                v-model="form.lastName"
                label="นามสกุล"
                :rules="[required]"
                variant="outlined"
                density="comfortable"
                color="blue-darken-2"
              />
            </v-col>

            <v-col cols="8">
              <v-text-field
                v-model="form.dateOfBirth"
                label="วันเกิด"
                type="date"
                :rules="[required]"
                variant="outlined"
                density="comfortable"
                color="blue-darken-2"
              />
            </v-col>

            <v-col cols="4">
              <v-text-field
                :model-value="`${age} ปี`"
                label="อายุ"
                readonly
                variant="outlined"
                density="comfortable"
                bg-color="grey-lighten-4"
              />
            </v-col>

            <v-col cols="12">
              <v-textarea
                v-model="form.address"
                label="ที่อยู่"
                rows="3"
                variant="outlined"
                density="comfortable"
                color="blue-darken-2"
              />
            </v-col>
          </v-row>
        </v-form>
      </v-card-text>

      <v-divider />

      <v-card-actions class="pa-4">
        <v-spacer />
        <v-btn color="error" variant="tonal" @click="emit('close')"
          >ยกเลิก</v-btn
        >
        <v-btn
          color="teal-darken-1"
          variant="elevated"
          :loading="saving"
          @click="handleSave"
          >บันทึก</v-btn
        >
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
