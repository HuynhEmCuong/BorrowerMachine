<template>
  <div class="modal fade " id="modealDepart" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title"> <i class="fa fa-television"></i> {{$t('department.NewDepart')}}</h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <div class="form-group row ">
            <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('department.NameDepart')}}</label>
            <div class="col-sm-8">
              <input type="text" v-model="nameDepart" class="form-control" >
            </div>
          </div>
          <div class="form-group row ">
            <label for="inputEmail3" class="col-sm-4 col-form-label">{{$t('department.Symbol')}}</label>
            <div class="col-sm-8">
              <input type="text" v-model="nameSys" class="form-control" >
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-danger" @click="AddDepart">{{$t('common.Add')}}</button>
          <button type="button" class="btn btn-secondary" data-dismiss="modal">{{$t('common.Close')}}</button>
        </div>
      </div>
      
    </div>
  </div>
</template>
<script>
  import axios from 'axios'
  export default {
    data() {
      return {
        nameDepart: '',
        nameSys: ''
      }
    },
    methods: {
      AddDepart() {
        if (this.CheckData()) {
          axios.post('/Depart/InsertDepart', { name: this.nameDepart, sys: this.nameSys }).then(res => {
            console.log(res.data);
            NotifySuccess(this.langIndex("alert.Depart_Add"))
            $("#modealDepart").modal("hide");
            this.$store.dispatch('getListDepart');
          }).catch(e => { console.log(e) })
        }
      },
      CheckData() {
        if (this.nameDepart == "") {
          NotifyDanger(this.langIndex("alert.Depart_Name"))
          return false;
        }
        else if (this.nameSys == "") {
          NotifyDanger(this.langIndex("alert.Depart_Symbol"));
          return false
        }
        else
          return true
      }
    }
  }
</script>

