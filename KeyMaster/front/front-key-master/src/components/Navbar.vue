<template>
  <v-app-bar elevation="6" class="navbar">
    <div class="nav">
      <router-link to="/"><h1 class="logo">KeyMaster</h1></router-link>
      <div class="nav-links">
        <router-link to="/Exercises"><h2>Ćwiczenia</h2></router-link>
        <h2 id="menu-activator">Lekcje</h2>
        <v-menu activator="#menu-activator">
          <v-list class="lessonList">
            <v-list-item v-for="lesson in lessons">
              <v-list-item-title>
                <router-link
                  :to="{ name: 'lesson', params: { lessonId: lesson.Id } }"
                  >Lekcja {{ lesson.Id }}
                  <p class="lesson-name">{{ lesson.Name }}</p>
                </router-link>
              </v-list-item-title>
            </v-list-item>
          </v-list>
        </v-menu>

        <router-link to="/test"><h2>Test szybkości</h2></router-link>
      </div>
      <div class="nav-signs-buttons">
        <div v-if="signIn" class="isloged">
          <router-link to="/profile"
            ><h2 class="userIsLogdOn">{{ username }}</h2></router-link
          >
          <button @click="logout()">Wyloguj się</button>
        </div>
        <div v-else>
          <router-link to="/signup">
            <button class="">Zaloguj się</button>
          </router-link>
        </div>
      </div>
    </div>
  </v-app-bar>
</template>
<style>
.isloged {
  display: inline-flex;
  align-items: center;
}

button {
  margin-left: 10px;
}
</style>
<script lang="ts">
import "@/styles/style.scss";
import "vue-router/dist/vue-router";
import { defineComponent } from "vue";

export default defineComponent({
  name: "NavbarComponent",
  data() {
    return {
      lessons: {},
      username: "",
      signIn: false,
    };
  },
  created() {
    this.getAllLessons();
    this.getUsername();
  },
  methods: {
    logout() {
      localStorage.clear();
      this.signIn = false;
    },
    getAllLessons() {
      this.axios
        .get("https://localhost:5001/api/lesson/allLessons")
        .then((resposne) => {
          this.lessons = resposne.data;
          //console.log(this.lessons);
        });
    },
    getUsername() {
      let token = "Bearer " + localStorage.getItem("token");
      this.axios
        .get("https://localhost:5001/api/user", {
          headers: {
            Authorization: token,
            "content-type": "text/json",
          },
        })
        .then((resposne) => {
          this.username = resposne.data.Nickname;
          this.signIn = true;
        });
    },
  },
});
</script>
