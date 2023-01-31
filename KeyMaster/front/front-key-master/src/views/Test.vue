<template>
  <NavbarVue></NavbarVue>
  <div class="test-title">
    <h1>Witaj na stronie testowej.</h1>
    <p>
      Tutaj będziesz mógł/mogła sprawdzić swoje postępy w praktycznym teście.
    </p>
  </div>
  <div class="lesson-container">
    <div class="quote">
      <h3>Ilość znaków: {{ length }}</h3>
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
        <div class="key" data-key="q" id="q">q</div>
        <div class="key" data-key="w" id="w">w</div>
        <div class="key" data-key="e" id="e">e</div>
        <div class="key" data-key="r" id="r">r</div>
        <div class="key" data-key="t" id="t">t</div>
        <div class="key" data-key="y" id="y">y</div>
        <div class="key" data-key="u" id="u">u</div>
        <div class="key" data-key="i" id="i">i</div>
        <div class="key" data-key="o" id="o">o</div>
        <div class="key" data-key="p" id="p">p</div>
      </div>
      <div class="row">
        <div class="key" data-key="a" id="a">a</div>
        <div class="key" data-key="s" id="s">s</div>
        <div class="key" data-key="d" id="d">d</div>
        <div class="key" data-key="f" id="f">f</div>
        <div class="key" data-key="g" id="g">g</div>
        <div class="key" data-key="h" id="h">h</div>
        <div class="key" data-key="j" id="j">j</div>
        <div class="key" data-key="k" id="k">k</div>
        <div class="key" data-key="lk" id="l">l</div>
        <div class="key" data-key=";" id="colon">;</div>
      </div>
      <div class="row">
        <div class="key special" data-key="shift" id="shift">shift</div>
        <div class="key" data-key="z" id="z">z</div>
        <div class="key" data-key="x" id="x">x</div>
        <div class="key" data-key="c" id="c">c</div>
        <div class="key" data-key="v" id="v">v</div>
        <div class="key" data-key="b" id="b">b</div>
        <div class="key" data-key="n" id="n">n</div>
        <div class="key" data-key="m" id="m">m</div>
        <div class="key" data-key="," id="coma">,</div>
        <div class="key" data-key="." id="dot">.</div>
      </div>
      <div class="row">
        <div class="key space" data-key=" " id="space">space</div>
        <div class="key" data-key="alt" id="alt">alt</div>
      </div>
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
  <FooterVue></FooterVue>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import NavbarVue from "../components/Navbar.vue";

import FooterVue from "../components/Footer.vue";
import { Chart } from "chart.js/auto";

export default defineComponent({
  name: "HelloWorld",
  components: {
    NavbarVue,
    FooterVue,
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
    $route: "fetchquote",
    inputText: "checkForMistakes",
  },
  methods: {
    fetchquote() {
      this.axios
        .get("https://localhost:5001/api/lesson/test")
        .then((resposne) => {
          console.log(resposne.data);
          this.source = resposne.data.Source;
          this.text = resposne.data.Text;
          this.text2 = resposne.data.Text.split("");
          this.length = resposne.data.Length;
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
            typeSpeed.push({ x: i, y: this.wpm });
          } else {
            errorpos.push({ x: failAt, y: errottmp });
            typeSpeed.push({ x: i, y: this.wpm });
            spanClass[i].setAttribute("class", "correct word");
          }
        }

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
      return cpm;
    },
    countAccuracy() {
      console.log((this.length - this.error) / this.length);
      let acc = ((this.length - this.error) / this.length) * 100;
      let accuracy = Math.round(acc);
      this.StatisticAccuracy = accuracy;
    },
    drawChart() {
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
          },
        },
      });
      myChart;
    },
  },
});
</script>
