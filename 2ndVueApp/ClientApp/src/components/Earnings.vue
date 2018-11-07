<template>
  <div>
    <h1 class="ui header">Points</h1>
    <button class="ui icon button" v-on:click="showModal()">
      <i class="add icon"></i>
    </button>
    <div class="ui modal">
      <div class="header">
        Add Points
      </div>
      <div class="content">
        <div class="ui form">
          <div class="field">
            <label>Date</label>
            <datepicker v-model="newPoint.earnedDate" id="earned_date"></datepicker>
          </div>
          <div class="ui grid">
            <div class="four wide column">
              <div class="ui dropdown" id="name_dd">
                <div class="text">Name</div>
                <i class="dropdown icon"></i>
              </div>
            </div>
            <div class="eight wide column">
              <div class="ui dropdown" id="rule_dd">
                <div class="text">Rule</div>
                <i class="dropdown icon"></i>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="actions">
        <div class="ui black deny button">
          Cancel
        </div>
        <div class="ui positive right labeled icon button">
          Save
          <i class="checkmark icon"></i>
        </div>
      </div>
    </div>
    <b-pagination size="md" :total-rows="totalRows" v-model="currentPage" :per-page="perPage" v-on:input="getEarnings">
    </b-pagination>
    <table class="ui celled table">
      <thead>
        <tr>
          <th>Date</th>
          <th>Name</th>
          <th>Points</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="point in points">
          <td>{{point.earnedDate}}</td>
          <td>{{point.name}}</td>
          <td>{{point.points}}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
  import axios from "axios";
  import Datepicker from 'vuejs-datepicker';

  export default {
    name: 'earnings',
    components: {
      Datepicker,
    },
    data() {
      return {
        points: [],
        newPoint: {
          earnedDate: "1/1/1970",
          name: "",
          points: ""
        },
        earners: [],
        errors: [],
        currentPage: 1,
        totalRows: 0,
        perPage: 10,
      }
    },
    methods: {
      showModal: function() {
        $('.ui.modal')
          .modal('show');
      },
      //getEarnings: function(pageNum, rowsPerPage, sortField) {
      getEarnings: function () {
        axios
          .get('api/earnings/page/' + this.$data.currentPage + '/rowsperpage/' + this.$data.perPage + '/sort/Name')
          .then(response => (this.points = response.data));
      },
      getEarningsCount: function () {
        axios
          .get('api/earnings/count')
          .then(response => (this.totalRows = response.data));
      }
    },
    mounted: function() {
      var self = this;
      self.getEarnings(1, 10, 'Name');
      self.getEarningsCount();

      //Set the modal approve and deny callbacks
      $('.ui.modal')
        .modal({
          closable: true,
          onDeny: function () {
            return;
          },
          onApprove: function () {
            /*****************************************
             * Add the new points using web API
             *****************************************/

            //Get the modal values
            var earned_date = $('#earned_date').val();
            var earner_id = $('#name_dd').dropdown('get value');
            var rule_id = $('#rule_dd').dropdown('get value');

            //Call the post route
            axios
              .post('api/earnings', { earnedDate: earned_date, earnerId: earner_id, ruleId: rule_id})
              .then(response => {})
              .catch(e => {
                console.log(e)
              })

            return;
          },
          onHidden: function () {
            self.getEarnings();
          }
        });

      //Load the name dropdown on the add new points modal
      $('.four.wide.column .ui.dropdown')
        .dropdown({
          apiSettings: {
            // this url just returns a list of tags (with API response expected above)
            url: 'api/earners/semantic_dd',
            cache: false
          },
        });

      //Load the rule dropdown on the add new points modal
      $('.eight.wide.column .ui.dropdown')
        .dropdown({
          apiSettings: {
            // this url just returns a list of tags (with API response expected above)
            url: 'api/rules/semantic_dd',
            cache: false
          },
        });
    },
    created: function () {
      // Remove the modal so it doesn't duplicate when the user navigates away from the component
      // and then returns to it
      $('.ui.modal').remove();
    }
  }
</script>
