<template>
  <div class="container">
    <div class="content">
      <div class="row">
        <div class="col-12" id="device">
          <div class="row page-titles">
            <div class="col-md-6 col-8 align-self-center">
              <h3 class="text-themecolor">  <i class="fa fa-arrow-circle-o-right"></i> &nbsp {{$t('history.HistoryBorrower')}}</h3>
            </div>

            <div class="col-md-6 col-4">
              <div class="row">
                <div class="col-8">

                  <Select2 v-model="idCate" :options="listCategoryDevice">

                  </Select2>

                </div>
                <div class="col-4">
                  <div class="pull-right">
                    <a class="btn btn-success" href="/History/ExportListHistory">
                      <i class="fa fa-file-excel-o">
                      </i> {{$t('common.ExportExcel')}}
                    </a>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="table-history mt-2">
            <table class="table table-bordered table-hover text-center">
              <thead>
                <tr>
                  <th scope="col">STT</th>
                  <th scope="col">{{$t('homepage.Department_Home')}}</th>
                  <th scope="col">{{$t('common.Category')}}</th>
                  <th scope="col"> {{$t('common.DeviceName')}}  </th>
                  <th scope="col">{{$t('common.Borrower')}} </th>
                  <th scope="col">{{$t('common.BorrowDate')}}</th>
                  <th scope="col">{{$t('common.DateReturn')}}</th>
                  <th scope="col"></th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="(history,index) in listHistory">
                  <th scope="row">{{index+1}}</th>
                  <td>{{history.DepartName}}</td>
                  <td>{{history.CateDeviceName}}</td>
                  <td>{{history.DevcieName}}</td>
                  <td>{{history.EmployName}}</td>
                  <td>{{ConvertDate(history.DateBorrow,"dd/mm/yyyy HH:MM")}}  </td>
                  <td>{{ConvertDate(history.DateReturn,"dd/mm/yyyy HH:MM")}}</td>
                  <td>
                    <i v-if="history.Status==3" class="fa fa-check-circle check"></i>
                    <i v-else class="fa fa-arrow-circle-o-down un-check"></i>
                  </td>
                </tr>
              </tbody>
            </table>
            <Paginate :page-count="totalPage"
                      :click-handler="changePage"
                      :prev-text="'Prev'"
                      :next-text="'Next'"
                      :container-class="'pagination justify-content-center'"
                      :page-class="'page-item'"
                      :page-link-class="'page-link'"
                      :prev-class="'page-item'"
                      :prev-link-class="'page-link'"
                      :next-class="'page-item'"
                      :next-link-class="'page-link'">
            </Paginate>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
  import axios from 'axios'
  import Paginate from 'vuejs-paginate';
  import Select2 from 'v-select2-component'
  export default {
    components: {
      Select2,
      Paginate
    },
    data() {
      return {
        listCategoryDevice: [],
        idCate: 0,
        page: 1,
        totalPage: 0,
        pageSize: 0,
        listHistory: []
      }
    },
    methods: {
      GetListCate() {
        axios.get('/home/GetListCategory').then(res => {
          let data = res.data;
          this.listCategoryDevice = data.map(x => {
            return { id: x.ID, text: x.NameCate }
          })
          this.listCategoryDevice.unshift({ id: 0, text: this.langIndex("common.ListCateDevice") })
        }).catch(e => { console.log(e) })
      },
      GetListHistory(id, page = 1) {
        axios.post('/History/GetAllHistory', { idCate: id, page: page }).then(res => {
          this.totalPage = res.data.TotalPage;
          this.pageSize = res.data.PageSize;
          this.page = res.data.CurrentPage;
          this.listHistory = res.data.Result;
          console.log(this.listHistory);
        })
      },
      changePage(pageNum) {
        this.GetListHistory(0, pageNum)
      },
      
    },
    created() {
       FadeLoadingBar();
      this.GetListCate();
      this.GetListHistory(0, 1);
    },
    watch: {
      idCate(newval) {
        this.GetListHistory(newval, 1)
      }
    }
  }
</script>
