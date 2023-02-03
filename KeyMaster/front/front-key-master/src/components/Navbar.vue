<template>
  <v-app-bar elevation="6" class="navbar">
    <div class="nav">
      <ul>
        <li>
          <router-link to="/" active-class="logo-active"
            ><h1 class="logo">KeyMaster</h1></router-link
          >
        </li>
        <li>
          <h2 id="menu-activator" active-class="navActive">Lekcje</h2>
          <v-menu activator="#menu-activator">
            <v-list class="lessonList">
              <v-list-item v-for="lesson in lessons">
                <v-list-item-title>
                  <router-link
                    :to="{ name: 'lesson', params: { lessonId: lesson.Id } }"
                    active-class="navActive"
                    >Lekcja {{ lesson.Id }}
                    <p class="lesson-name">{{ lesson.Name }}</p>
                  </router-link>
                </v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>
        </li>
        <li>
          <router-link to="/test" active-class="navActive"
            ><h2>Test szybkości</h2></router-link
          >
        </li>
        <li>
          <router-link to="/help" active-class="navActive"
            ><h2>Pomoc</h2></router-link
          >
        </li>
      </ul>

      <div class="nav-links"></div>
      <div class="nav-signs-buttons">
        <div v-if="signIn" class="isloged">
          <router-link to="/profile"
            ><h2 class="userIsLogdOn">{{ username }}</h2></router-link
          >
          <button @click="logout()">Wyloguj się</button>
        </div>
        <div v-else>
          <router-link to="/signup">
            <button>Zaloguj się</button>
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
      isActive: false,
    };
  },
  created() {
    this.getAllLessons();
    this.getUsername();
  },
  methods: {
    setactive() {
      var li = document.getElementsByClassName("link");
    },
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
