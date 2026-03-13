import { Component } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Page } from '../../interfaces/page';
import { CommonModule } from '@angular/common';
import { UsersService } from '../../../users/services/users.service';
import { UserResponse } from '../../../users/models/responses/user.response';
import { Router, RouterModule } from '@angular/router';
import { debounceTime } from 'rxjs';

@Component({
  selector: 'app-header',
  imports: [
    ReactiveFormsModule,
    FormsModule,
    CommonModule,
    RouterModule
  ],

  standalone: true,
  templateUrl: './header.component.html'
})
export class HeaderComponent {
  readonly pagesSet: Page[] = [
    { Name: "Início", Url: "hub", Icon: "bi bi-house-door-fill" },
    { Name: "Registrar Venda", Url: "vendas", Icon: "bi bi-basket2" },
    { Name: "Produtos e Estoque", Url: "produtos", Icon: "bi bi-box-seam" },
    { Name: "Financeiro e Histórico", Url: "finaceiro/historico-operacoes", Icon: "bi bi-bar-chart-line" },
    { Name: "Usuários", Url: "usuarios", Icon: "bi bi-people" },
    { Name: "Configurações", Url: "configuracoes", Icon: "bi bi-gear" },
  ]

  pages: Page[] = [];
  form: FormGroup;
  user: UserResponse;
  currentUrl: string = '';

  constructor(public usersService: UsersService, private router: Router) {
    this.form = new FormGroup({
      searchInput: new FormControl(''),
    });
  }

  ngOnInit(): void {
    this.pages = this.pagesSet;
    this.getCurrentUser();
    this.currentUrl = this.router.url;
    this.form.get('searchInput').valueChanges
    .pipe(debounceTime(300))
    .subscribe(() => {this.search()})
  }

  public search() {
    this.pages = this.pagesSet.filter(p => p.Name.normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase().includes(this.form.value.searchInput.normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase()));
  }

  public clear() {
    if (this.form.value.searchInput == '') {
      this.pages = this.pagesSet
    }
  }

  public getCurrentUser()
  {
    this.usersService.currentUser$
    .subscribe({
      next: (response) => {
        this.user = response;
      }
    });
  }

  public logout()
  {
    this.usersService.logout();
    this.router.navigateByUrl('/login');
  }
}
