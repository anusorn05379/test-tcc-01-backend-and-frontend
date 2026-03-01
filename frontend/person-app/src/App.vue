<script setup lang="ts">
import PersonTable from "./components/PersonTable.vue";
</script>

<template>
  <v-app>
    <v-app-bar color="blue-darken-2" elevation="4" flat>
      <v-toolbar-title class="text-white font-weight-bold text-subtitle-1">
        ระบบจัดการข้อมูลบุคคล
      </v-toolbar-title>
    </v-app-bar>
    <v-main class="bg-grey-lighten-4">
      <PersonTable />
    </v-main>
  </v-app>
</template>
