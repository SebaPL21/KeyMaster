<template xmlns="http://www.w3.org/1999/html">
  <Navbar></Navbar>
  <v-container>
    <div class="content">
      <div class="userMainStats">
        <v-avatar size="100">
          <v-img src="../../public/logo.png" />
        </v-avatar>
        <h3>Średnia prędkość: {{ AvgSpeed }}</h3>
        <h3>Średnia dokłacność: {{ AvgAccuracy }}%</h3>
      </div>
      <div>
        <v-table class="v-table v-theme--dark v-table--density-default">
          <thead>
            <tr>
              <th class="text-left">Tytul</th>
              <th class="text-left">Średnia prędkość WPM</th>
              <th class="text-left">Błędy</th>
              <th class="text-left">Dokładność</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in scores" :key="item.id">
              <td>{{ item.TextTitle }}</td>
              <td>{{ item.Cpm }}</td>
              <td>{{ item.Errors }}</td>
              <td>{{ item.Accuracy }}</td>
            </tr>
          </tbody>
        </v-table>
      </div>
    </div>
  </v-container>
  <FooterVue></FooterVue>
</template>

<script lang="ts">
import "@/styles/profileStyle.scss";
import Navbar from "../components/Navbar.vue";
import FooterVue from "../components/Footer.vue";
import { defineComponent } from "vue";

export default defineComponent({
  name: "Profile",
  components: {
    Navbar,
    FooterVue,
  },
  data() {
    return {
      scores: [],
      textTitle: "",
      accuracy: 0,
      cpm: 0,
      error: 0,
      AvgSpeed: 0,
      AvgAccuracy: 0,
      accutacyArray: [],
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
          this.AvgScores();
          this.countAvgSpeed();
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
          //console.log(response);
        });
    },
    AvgScores() {
      let avgAcc;
      let numbers = [];
      let scores = this.scores;
      let sum = 0;
      scores.forEach((item) => {
        numbers.push(item.Accuracy);
        // console.log(item);
      });
      for (let i = 0; i < numbers.length; i++) {
        if (isNaN(numbers[i])) {
          console.log(numbers[i]);
        } else {
          sum += parseInt(numbers[i].toString());
          this.AvgAccuracy = Math.round(sum / numbers.length);
        }
      }
    },
    countAvgSpeed() {
      let speeds = [];
      let speed = this.scores;
      let sum = 0;
      speed.forEach((item) => {
        speeds.push(item.Cpm);
        //console.log(item);
        //console.log(speeds);
      });
      for (let i = 0; i < speeds.length; i++) {
        if (isNaN(speeds[i])) {
          console.log(speeds[i]);
        } else {
          sum += parseInt(speeds[i].toString());
          // console.log(parseInt(speeds[i].toString()));
          // console.log(sum);
          this.AvgSpeed = Math.round(sum / speeds.length);
        }
      }
    },
  },
});
</script>

<style>
.profile img {
  width: 200px;
  border-radius: 50%;
}
</style>
