<template>
  <Navbar></Navbar>
  <div class="lesson-container">
    <div class="quote">
      <h3 class="mb-5">
        Ilość znaków: {{ length }}
        <v-btn
          outlined
          color="#C03E3FFF"
          class="float-end me-5 reloadButton"
          @click="ForcesUpdateComponent()"
          >Losuj nowy tekst
        </v-btn>
      </h3>

      <span class="word" v-for="text in text2">
        {{ text }}
      </span>
      <div class="autor">
        {{ source }}
      </div>
    </div>
    <div class="inputlesson">
      <input
        type="text"
        class="wordInput"
        v-model="inputText"
        :maxlength="length"
        :disabled="isEnabled"
      />
    </div>
    <div id="keyboard">
      <div class="row">
        <div class="key redKey" data-key="q" id="q">q</div>
        <div class="key orangeKey" data-key="w" id="w">w</div>
        <div class="key greenKey" data-key="e" id="e">e</div>
        <div class="key blueKey" data-key="r" id="r">r</div>
        <div class="key blueKey" data-key="t" id="t">t</div>
        <div class="key lightBlueKey" data-key="y" id="y">y</div>
        <div class="key lightBlueKey" data-key="u" id="u">u</div>
        <div class="key lightGreen" data-key="i" id="i">i</div>
        <div class="key yellowKey" data-key="o" id="o">o</div>
        <div class="key maroonKey" data-key="p" id="p">p</div>
      </div>
      <div class="row">
        <div class="key redKey" data-key="a" id="a">a</div>
        <div class="key orangeKey" data-key="s" id="s">s</div>
        <div class="key greenKey" data-key="d" id="d">d</div>
        <div class="key blueKey" data-key="f" id="f">f</div>
        <div class="key blueKey" data-key="g" id="g">g</div>
        <div class="key lightBlueKey" data-key="h" id="h">h</div>
        <div class="key lightBlueKey" data-key="j" id="j">j</div>
        <div class="key lightGreen" data-key="k" id="k">k</div>
        <div class="key yellowKey" data-key="l" id="l">l</div>
        <div class="key maroonKey" data-key=";" id="colon">;</div>
      </div>
      <div class="row">
        <div class="key redKey" data-key="shift" id="shift">shift</div>
        <div class="key redKey" data-key="z" id="z">z</div>
        <div class="key orangeKey" data-key="x" id="x">x</div>
        <div class="key greenKey" data-key="c" id="c">c</div>
        <div class="key blueKey" data-key="v" id="v">v</div>
        <div class="key blueKey" data-key="b" id="b">b</div>
        <div class="key lightBlueKey" data-key="n" id="n">n</div>
        <div class="key lightBlueKey" data-key="m" id="m">m</div>
        <div class="key lightGreen" data-key="," id="coma">,</div>
        <div class="key yellowKey" data-key="." id="dot">.</div>
      </div>
      <div class="row">
        <div class="key space" data-key=" " id="space">space</div>
        <div class="key alt" data-key="alt" id="alt">alt</div>
      </div>
      <v-img src="../assets/hands.jpg" height="229" width="461" />
    </div>
    <div>
      <div class="chart">
        <canvas id="myChart"></canvas>
      </div>
      <div class="afterLessonStats" v-if="afterLesson">
        <v-btn variant="outlined" color="#C03E3FFF" @click="exportToCsv()"
          >Pobierz statystyki
        </v-btn>
        <h2>Popełnione błędy {{ error }}</h2>
        <h2>Dokładność {{ StatisticAccuracy }} %</h2>
        <h2>CPM {{ StatisticClicksPerMinute }}</h2>
        <h2>Czas pisania {{ time }}s</h2>
      </div>
    </div>
  </div>
  <Footer></Footer>
</template>
<style>
html::-webkit-scrollbar-thumb {
  width: 44px;
}

html::-webkit-scrollbar-track {
  background-color: #b91212;
}

html::-webkit-scrollbar-thumb {
  background-color: #222222;
}

::-webkit-scrollbar-thumb:hover {
  background-color: #222222;
}
</style>
<script lang="ts">
import { Options, Vue } from "vue-class-component";
import Navbar from "@/components/Navbar.vue";
import Footer from "@/components/Footer.vue";
import "/src/styles/style.scss";
import { defineComponent, onMounted, ref } from "vue";
import { Chart } from "chart.js/auto";
import Papa from "papaparse";
import * as events from "events";

export default defineComponent({
  // eslint-disable-next-line vue/multi-word-component-names
  name: "Exercises",
  components: {
    Navbar,
    Footer,
  },
  data() {
    return {
      result: "",
      text: "",
      text2: [],
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
      TypingSpeed: [{ x: 0, y: 0 }],
      afterLesson: false,
      timeoutId: 0,
      labels: [""],
      labelsSpeed: [""],
      time: 0,
      keyList: [],
      wpm: 0,
      iretator:1,
      lettersDictionary: {
        a: ["a"],
        ą: ["alt", "a"],
        b: ["b"],
        c: ["c"],
        ć: ["alt", "c"],
        d: ["d"],
        e: ["e"],
        ę: ["alt", "e"],
        f: ["f"],
        g: ["g"],
        h: ["h"],
        i: ["i"],
        j: ["j"],
        k: ["k"],
        l: ["l"],
        ł: ["alt", "l"],
        m: ["m"],
        n: ["n"],
        ń: ["alt", "n"],
        o: ["o"],
        ó: ["alt", "o"],
        p: ["p"],
        q: ["q"],
        r: ["r"],
        s: ["s"],
        ś: ["alt", "s"],
        t: ["t"],
        u: ["u"],
        v: ["v"],
        w: ["w"],
        x: ["x"],
        y: ["y"],
        z: ["z"],
        ż: ["alt", "z"],
        ",": ["coma"],
        " ": ["space"],
        alt: ["alt"],
        ":": ["shift", "colon"],
        ";": ["colon"],
        ".": ["dot"],
      },
    };
  },
  created() {
    this.fetchquote();
  },
  watch: {
    $route: "fetchData",
    inputText: "checkForMistakes",
  },

  methods: {
    ForcesUpdateComponent() {
      window.location.reload();
    },
    fetchquote() {
      this.axios
        .get("https://localhost:5001/api/quotes/fetch")
        .then((resposne) => {
          //console.log(resposne.data);
          this.source = resposne.data.Source;
          this.text = resposne.data.Text;
          this.length = resposne.data.Length;
          this.text2 = resposne.data.Text.split("");
          this.checkForMistakes();
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
      let avgSpeed = this.countClicksPerMinute();
      var textArray = this.text.split("");
      var inputTextArray = this.inputText.split("");
      var errottmp = 0;
      var failAt = 0;
      let errorpos = [{ x: 0, y: 0 }];
      let typeSpeed = [{ x: 0, y: 0 }];
      let spanClass = document.getElementsByClassName("word");
      Array.from(spanClass).forEach((element) => {
        element.setAttribute("class", "word");
      });
      for (let i = -1; i < inputTextArray.length; i++) {
        if (i >= 0) {
          if (inputTextArray[i] != textArray[i]) {
            errottmp++;
            this.statisticErrors++;
            failAt = i;
            spanClass[i].setAttribute("class", "incorect word");
            errorpos.push({ x: failAt, y: errottmp });
          } else {
            errorpos.push({ x: failAt, y: errottmp });
            spanClass[i].setAttribute("class", "correct word");
          }
          
        }
        typeSpeed.push({ x: i, y: avgSpeed });
        let nextLetter = textArray[i + 1];
        let clickedKeys = document.getElementsByClassName("key active");
        let keyCodes = Object.keys(this.lettersDictionary).map((element) => {
          return element.charCodeAt(0);
        });
        Array.from(clickedKeys).forEach((x) => {
          x.classList.remove("active");
        });
        if (nextLetter == undefined) {
          break;
        } else {
          if (keyCodes.includes(nextLetter.toLowerCase().charCodeAt(0))) {
            this.lettersDictionary[nextLetter.toLowerCase()].forEach(
              (element) => {
                document.getElementById(element).classList.add("active");
              }
            );
            if (nextLetter.match(/[A-ZĄĆĘŁŃÓŚŹŻ]/)) {
              document.getElementById("shift").classList.add("active");
            }
          }
        }
      }
      
      this.error = errottmp;
      this.errorPosition = errorpos;
      this.TypingSpeed = typeSpeed;
      this.StatisticKeyboardClicks++;
      if (inputTextArray.length != 0 && inputTextArray.length == this.length) {
        this.isEnabled = true;
        this.afterLesson = true;
        this.countAccuracy();
        this.errorPosition.forEach((i) => {
          this.labels.push(i.x.toString());
        });
        this.TypingSpeed.forEach((s) => {
          this.labelsSpeed.push(s.x.toString());
        });
        this.drawChart();
        this.addtoscores();
      }
    },

    countClicksPerMinute() {
      this.clicks++;
      let currentTime = Date.now();
      let elapsedTime = (currentTime - this.startTime) / 1000;
      let clicksPerMinute = this.clicks / (elapsedTime / 60);
      let cpm = Math.round(clicksPerMinute);
      this.time = Math.round((currentTime - this.startTime) / 1000);
      this.StatisticClicksPerMinute = cpm;
      this.wpm = cpm / 5;
      return cpm/5;
    },
    countAccuracy() {
      let acc = ((this.length - this.error) / this.length) * 100;
      let accuracy = Math.round(acc);
      this.StatisticAccuracy = accuracy;
    },
    drawChart() {
      console.log(this.TypingSpeed);
      const ctx = "myChart";
      const data = {
        labels: this.labelsSpeed,
        datasets: [
          {
            label: "Błędy",
            data: this.errorPosition,
            borderColor: "rgb(192,75,75)",
            tension: 0.1,
          },
          {
            label: "Wpm",
            data: this.TypingSpeed,
            borderColor: "rgb(54,122,224)",
            tension: 0.1,
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
            x: {
              beginAtZero: true,
            },
          },
        },
      });
      myChart;
    },
    exportToCsv() {
      const data = [
        {
          fixedErrors: this.error,
          errors: this.statisticErrors,
          avgAccuracy: this.StatisticAccuracy,
          time: this.time,
          CPM: this.StatisticClicksPerMinute,
          WPm: this.wpm,
          title: this.source,
        },
      ];
      const csv = Papa.unparse(data);
      const blob = new Blob([csv]);
      const link = document.createElement("a");
      link.href = window.URL.createObjectURL(blob);
      link.download = "data.csv";
      link.click();
    },
  },
});
</script>
