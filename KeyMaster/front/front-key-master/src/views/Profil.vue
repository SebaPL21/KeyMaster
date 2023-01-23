<template>
  <Navbar></Navbar>
  <v-container>
    <div class="content">
      <div class="userMainStats" v-for="item in avgScores">
        <v-avatar size="100">
          <v-img src="../assets/logo2.png"></v-img>
        </v-avatar>
        <h3>Średnia prędkość {{ item.avgSpeed }}</h3>
        <h3>Średnia dokłacność {{ item.avgAccuracy }}</h3>
      </div>

      <v-table>
        <thead>
          <tr>
            <th class="text-left">Tytul</th>
            <th class="text-left">Średnia prędkość WPM</th>
            <th class="text-left">Błędy</th>
            <th class="text-left">Regularność</th>
            <th class="text-left">Ocena</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in scores" :key="item.id">
            <td>{{ item.TextTitle }}</td>
            <td>{{ item.Wpm }}</td>
            <td>{{ item.Errors }}</td>
            <td>{{ item.Consistency }}</td>
            <td>{{ item.Mark }}</td>
          </tr>
        </tbody>
      </v-table>
    </div>
  </v-container>
  <FooterVue></FooterVue>
</template>

<script lang="ts">
import "@/styles/profileStyle.scss";
import Navbar from "../components/Navbar.vue";
import FooterVue from "../components/Footer.vue";
import { defineComponent } from "vue";
import axios from "axios";

export default defineComponent({
  name: "Profile",
  components: {
    Navbar,
    FooterVue,
  },
  data() {
    return {
      scores: {},
      textTitle: "",
      consistency: 0,
      wpm: 0,
      mark: 0,
      error: 0,
      Date: "",
      AvgScores: {
        AvgSpeed: 0,
        AvgAccuracy: 0,
      },
    };
  },
  created() {
    this.getStatistics();
    this.getProfile();
  },
  methods: {
    getStatistics() {
      let token = "Bearer " + localStorage.getItem("token");
      this.axios
        .get("https://localhost:5001/api/ScoresModels/user", {
          headers: {
            Authorization: token,
          },
        })
        .then((resposne) => {
          this.scores = resposne.data;
          console.log(resposne.data);
        });
    },
    getProfile() {
      let token = "Bearer " + localStorage.getItem("token");
      this.axios
        .get("https://localhost:5001/api/user", {
          headers: {
            Authorization: token,
          },
        })
        .then((response) => {
          console.log(response);
        });
    },
  },
});
</script>

<style>
.profile {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.profile img {
  width: 200px;
  border-radius: 50%;
}
</style>
