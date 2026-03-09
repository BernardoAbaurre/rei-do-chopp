import { BsModalService } from 'ngx-bootstrap/modal';
import { UsersService } from './../../services/users.service';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { LoginRequest } from '../../models/requests/login.request';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs';
import { ForgotPasswordModalComponent } from '../../components/forgot-password-modal/forgot-password-modal.component';

@Component({
  standalone: true,
  selector: 'app-login',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit{
  formLogin: FormGroup;
  submitted = false;

  constructor(private fb: FormBuilder, private usersService : UsersService, private router: Router, private toastr: ToastrService, private spinner: NgxSpinnerService, private bsModalService: BsModalService) {
    this.formLogin = this.fb.group({
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', [Validators.required]],
    });
  }
  ngOnInit(): void {
    this.usersService.getCurrentUser()
   .subscribe(response => {
      if(response)
      {
        this.router.navigateByUrl('/hub');
      }
      else
      {
        this.checkDbOn();
      }
    })
  }

  public checkDbOn(count: number = 0)
  {
    if(count == 3){
      this.toastr.error("Não foi possível conectar-se ao servidor");
      this.spinner.hide()
      return;
    }
    else { this.spinner.show(); }


    this.usersService.dbTest()
    .subscribe({
      next: (response) => {
        if(!response)
        {
          setTimeout(() => {this.checkDbOn(count + 1)}, 1000 * count);
        }
        else { this.spinner.hide() }
      },
      error: (err) => { this.spinner.hide() }
    });
  }

  onSubmit() {
    this.submitted = true;

    if (this.formLogin.invalid) {
      return;
    }

    this.spinner.show();
    this.usersService.login(new LoginRequest(this.formLogin.value))
    .pipe(finalize(() => this.spinner.hide()))
    .subscribe((response) =>{
      this.usersService.saveTokenOnCookies(response.Token);
      this.router.navigateByUrl('/hub');
    })
  }

  public openForgotPasswordModal()
  {
    this.bsModalService.show(ForgotPasswordModalComponent)
  }
}
