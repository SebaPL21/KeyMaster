<template>
  <v-app-bar elevation="6" class="navbar">
    <div class="nav">
      <router-link to="/"><h1 class="logo">KeyMaster</h1></router-link>
      <div class="nav-links">
        <h2 id="menu-activator">Lekcje</h2>
        <v-menu activator="#menu-activator">
          <v-list class="lessonList">
            <v-list-item v-for="lesson in lessons">
              <v-list-item-title>
                <router-link
                  :to="{ name: 'lesson', params: { lessonId: lesson.Id } }"
                  >{{ lesson.Name }}
                </router-link>
              </v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>

        <router-link to="/test"><h2>Test</h2></router-link>
      </div>
      <div class="nav-signs-buttons">
        <router-link to="/signup">
          <button class="">Zaloguj się</button>
        </router-link>
        <div class="isloged">
          <button class="" @click="logout()">Wyloguj się</button>
        </div>
      </div>
    </div>
  </v-app-bar>
</template>
<style>
.isloged {
  display: none;
}
</style>
<script lang="ts">
import "@/styles/style.scss";
import axios from "axios";
import "vue-router/dist/vue-router";
import router from "@/router";
import { defineComponent } from "vue";

export default defineComponent({
  name: "NavbarComponent",
  data() {
    return {
      lessons: {},
    };
  },
  created() {
    this.getAllLessons();
  },
  methods: {
    getAllLessons() {
      this.axios
        .get("https://localhost:5001/api/lesson/allLessons")
        .then((resposne) => {
          this.lessons = resposne.data;
          //console.log(this.lessons);
        });
    },
  },
});
</script>
