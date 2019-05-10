
var app = angular.module('app', ['ngRoute']);

app.controller('indexController', function($scope, $route, $routeParams, $location, customerService) {
  this.$onInit = function () {
    getAllCustomers();
  };
  
  async function getAllCustomers() {
    await customerService.getAllCustomers()
    .then((response) =>{
      $scope.customers = response.data;
    }).catch((error) => {
    console.log('error');
    });
  }

  this.deleteCustomer = async function(customer) {
    customerService.deleteCustomer(customer.id)
    .then((response) =>{
      $route.reload()
    }).catch((error) => {
    console.log('error');
    });
  }

  this.newCustomer = function() {
    $location.path('/cliente/new');  
  }

  this.editCustomer = function(customer) {
    $location.path('/cliente/' + customer.id);
  }

});

app.controller('customerController', function($scope, $routeParams, $location, customerService) {
  this.$onInit = function() {
    $scope.customer = {};
    var id = $routeParams.id;

    if((id != null || id != undefined) && (id != "new")){
      getCustomer(id);
    }
  }

  async function getCustomer(id){
    customerService.getCustomer(id)
    .then(function(response){
      $scope.customer = response.data;
    }).catch(function(error) {
      console.log(error);
    })
  }

  this.save = function (customer) {
    debugger;
    if(customer.id != null || customer.id != undefined){
      customerService.updateCustomer(customer)
      .then(function (response) {
        $location.path('/'); 
      }).catch(function(error) {
        console.log(error);
      });
    }else{
      customerService.createCustomer(customer)
      .then(function (response) {
        $location.path('/'); 
      }).catch(function(error) {
        console.log(error);
      });
    }
  }
})

app.service('customerService', function($http){

  const URL = 'http://localhost:5000';
  const HEADERS = {
    'Content-Type': 'application/json',
  }

  this.getAllCustomers = function(){
    return $http({
      method: 'GET',
      url: URL + '/api/customers',
      headers: HEADERS
    });
  }

  this.getCustomer = function(id) {
    return $http({
      method: 'GET',
      url: URL + '/api/customers/' + id,
      headers: HEADERS
    });
  }

  this.deleteCustomer = function(id) {
    return $http({
      method: 'DELETE',
      url: URL + '/api/customers/' + id,
      headers: HEADERS
    });
  }

  this.updateCustomer = function(customer) {
    return $http({
      method: 'PUT',
      url: URL + '/api/customers/' + customer.id,
      headers: HEADERS,
      data: { customer: customer }
    });
  }

  this.createCustomer = function(customer) {
    return $http({
      method: 'POST',
      url: URL + '/api/customers',
      headers: HEADERS,
      data: { customer: customer }
    });
  }
});

app.config(function($routeProvider){
  $routeProvider
    .when('/', { // localhost:8080/#/cliente/all
      templateUrl: 'app/views/customerAll.html'
    })
   .when('/cliente/:id', {
    templateUrl: 'app/views/customerForm.html',
    controller: 'customerController'
    })
    .when('/cliente/new', {
      templateUrl: 'app/views/customerForm.html',
      controller: 'customerController'
    });
});

  


