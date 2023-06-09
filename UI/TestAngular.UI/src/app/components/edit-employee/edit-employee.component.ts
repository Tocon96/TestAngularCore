import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from '../../models/employee.model';
import { EmployeesService } from '../../services/employees.service';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {

  departments: string[] = [
    'HR',
    'IT',
    'CEO'
  ]

  employeeDetails: Employee = {
    id: '',
    name: '',
    email: '',
    phone: 0,
    salary: 0,
    department: ''
  };

  constructor(private route: ActivatedRoute, private employeeService: EmployeesService, private router: Router) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');

        if (id) {
          this.employeeService.getEmployee(id).subscribe({
            next: (response) => {
              this.employeeDetails = response;
            }
          });
        }
      }
    })
  }

  updateEmployee() {
    this.employeeService.updateEmployee(this.employeeDetails.id, this.employeeDetails)
      .subscribe({
        next: (employee) => {
          this.router.navigate(['employees']);
        },
        error: (response) => {
          console.log(response);
        }
      });
  }

  deleteEmployee(id: string) {
    this.employeeService.deleteEmployee(this.employeeDetails.id)
      .subscribe({
        next: (employee) => {
          this.router.navigate(['employees']);
        },
        error: (response) => {
          console.log(response);
        }
      });
  }

}
