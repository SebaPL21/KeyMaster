<template>
  <Navbar></Navbar>
  <div class="test-title">
    <h1>Witaj</h1>
    <h1>tutaj bedziesz ćwiczyć na którtkich tesktach</h1>
  </div>
  <div class="lesson-container">
    <div class="quote">
      {{ text }}
      <div class="autor">
        {{ source }}
        {{ length }}
      </div>
    </div>
    <!--    {{ StatisticKeyboardClicks }}-->
    <div class="inputlesson">
      <input
        type="text"
        class="wordInput"
        v-model="inputText"
        :maxlength="length"
        :disabled="isEnabled"
        v-on:keyup="checkForMistakes()"
      />
    </div>

    <!--    <div v-if="afterLesson">-->
    <!--      <Chart errorPosition="errorPosition" errors="error"></Chart>-->
    <!--      <h2>Popełnione błędy {{ statisticErrors }}</h2>-->
    <!--      <h2>Dokładność {{ StatisticAccuracy }} %</h2>-->
    <!--    </div>-->
    <div>
      <div class="chart">
        <canvas id="myChart"></canvas>
      </div>
      {{ error }}
      <h2>Popełnione błędy {{ statisticErrors }}</h2>
      <h2>Dokładność {{ StatisticAccuracy }} %</h2>
      <h2>CPM {{ StatisticClicksPerMinute }}</h2>
    </div>
  </div>
  <Footer></Footer>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import Navbar from "@/components/Navbar.vue";
import Footer from "@/components/Footer.vue";

import { defineComponent, onMounted, ref } from "vue";
import { Chart } from "chart.js/auto";

export default defineComponent({
  components: {
    Navbar,
    Footer,
  },
  data() {
    return {
      result: "",
      text: "",
      source: "",
      length: 0,
      inputText: "",
      error: 0,
      statisticErrors: 0,
      StatisticKeyboardClicks: -1,
      StatisticClicksPerMinute: 0,
      StatisticAccuracy: 0,
      Mark: 0,
      isEnabled: false,
      clicks: 0,
      startTime: Date.now(),
      errorPosition: [{ x: 0, y: 0 }],
      afterLesson: false,
      timeoutId: 0,
    };
  },
  created() {
    this.fetchquote();
    this.checkForMistakes();
  },
  watch: {
    // call again the method if the route changes
    $route: "fetchData",
  },
  methods: {
    fetchquote() {
      this.axios
        .get("https://localhost:5001/api/quotes/fetch")
        .then((resposne) => {
          console.log(resposne.data);
          this.source = resposne.data.Source;
          this.text = resposne.data.Text;
          this.length = resposne.data.Length;
        });
    },
    addtoscores() {
      let token = "Bearer " + localStorage.getItem("token");
      this.axios.post(
        "https://localhost:5001/api/ScoresModels",
        {
          Cpm: this.StatisticKeyboardClicks,
          Errors: this.error,
          Accuracy: this.StatisticAccuracy.toFixed(0),
          TextTitle: this.source,
        },
        {
          headers: {
            Authorization: token,
            "content-type": "text/json",
          },
        }
      );
    },
    checkForMistakes() {
      this.countClicksPerMinute();
      var textArray = this.text.split("");
      var inputTextArray = this.inputText.split("");
      var errottmp = 0;
      var failAt = 0;
      let errorpos = [{ x: 0, y: 0 }];
      for (let i = 0; i < inputTextArray.length; i++) {
        if (inputTextArray[i] != textArray[i]) {
          errottmp++;
          this.statisticErrors++;
          console.log("błąd");
          failAt = i;
          errorpos.push({ x: failAt, y: errottmp });
        } else {
          console.log("git ");
        }
      }
      this.errorPosition = errorpos;
      console.log(this.errorPosition);
      this.StatisticKeyboardClicks++;
      if (inputTextArray.length != 0 && inputTextArray.length == this.length) {
        this.isEnabled = true;
        this.afterLesson = true;
        this.countAccutacy();
        this.drawChart();
        this.addtoscores();
      }
      this.error = errottmp;
    },
    countClicksPerMinute() {
      this.clicks++;
      let currentTime = Date.now();
      let elapsedTime = (currentTime - this.startTime) / 1000;
      let clicksPerMinute = this.clicks / (elapsedTime / 60);
      let cpm = Math.round(clicksPerMinute);
      this.StatisticClicksPerMinute = cpm;
    },
    countAccutacy() {
      console.log((this.length - this.statisticErrors) / this.length);
      let acc = ((this.length - this.statisticErrors) / this.length) * 100;
      let accuracy = Math.round(acc);
      this.StatisticAccuracy = accuracy;
    },
    drawChart() {
      const ctx = "myChart";
      const labels = [
        "0",
        "50",
        "100",
        "150",
        "200",
        "250",
        "300",
        "350",
        "400",
        "450",
        "500",
      ];
      const data = {
        labels: labels,
        datasets: [
          {
            label: "Wykres blędów",
            data: this.errorPosition,
            fill: false,
            borderColor: "rgb(192,75,75)",
            tension: 1,
          },
        ],
      };
      const myChart = new Chart(ctx, {
        type: "line",
        data: data,
        options: {
          scales: {
            y: {
              beginAtZero: true,
            },
          },
        },
      });
      myChart;
    },
  },
  // mounted() {
  //   const ctx = "myChart";
  //   const labels = [
  //     "0",
  //     "50",
  //     "100",
  //     "150",
  //     "200",
  //     "250",
  //     "300",
  //     "350",
  //     "400",
  //     "450",
  //     "500",
  //   ];
  //   const data = {
  //     labels: labels,
  //     datasets: [
  //       {
  //         label: "Wykres blędów",
  //         // data: [
  //         //   { x: 0, y: 0 },
  //         //   { x: 47, y: 1 },
  //         //   { x: 54, y: 2 },
  //         // ],
  //         data: this.errorPosition,
  //         fill: false,
  //         borderColor: "rgb(192,75,75)",
  //         tension: 1,
  //       },
  //     ],
  //   };
  //   const myChart = new Chart(ctx, {
  //     type: "line",
  //     data: data,
  //     options: {
  //       scales: {
  //         y: {
  //           beginAtZero: true,
  //         },
  //       },
  //     },
  //   });
  //   myChart;
  // },
});
</script>
