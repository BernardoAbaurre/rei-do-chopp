import { Component } from '@angular/core';
import { UserListRequest } from '../../models/requests/user-list.request';
import { UsersService } from '../../services/users.service';
import { FormGroup, FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { debounceTime, finalize } from 'rxjs';
import { OrdenationTypeEnum } from '../../../shared/enums/ordenation-type.enum';
import { PaginationResponse } from '../../../shared/models/responses/pagination.response';
import { UserResponse } from '../../models/responses/user.response';
import { SortButtonComponent } from "../../../shared/components/sort-button/sort-button.component";
import { CommonModule } from '@angular/common';
import { NgSelectModule } from '@ng-select/ng-select';
import { RoleResponse } from '../../../roles/models/responses/role.response';
import { ActiveBadgeComponent } from "../../../shared/components/active-badge/active-badge.component";
import { PaginationComponent } from "../../../shared/components/pagination/pagination.component";
import { RolesService } from '../../../roles/services/roles.service';
import { ProductsSelectComponent } from "../../../products/components/products-select/products-select.component";
import { RolesSelectComponent } from "../../../roles/components/roles-select/roles-select.component";
import { UserRegisterModalComponent } from '../../components/user-register-modal/user-register-modal.component';

@Component({
  selector: 'app-users-list',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SortButtonComponent,
    NgSelectModule,
    ActiveBadgeComponent,
    PaginationComponent,
    ProductsSelectComponent,
    RolesSelectComponent
  ],
  templateUrl: './users-list.component.html',
  styleUrl: './users-list.component.css'
})
export class UsersListComponent {
  usersRequest = new UserListRequest({});
  roles: RoleResponse[] = [];
  filterForm: FormGroup;
  response: PaginationResponse<UserResponse> = new PaginationResponse<UserResponse>();
  userRegisterModalRef: BsModalRef;

  statusOptions = [
    { Description: 'Ativo', Value: true },
    { Description: 'Inativo', Value: false },
  ];

  constructor(private usersService: UsersService, private fb: FormBuilder, private bsModalService: BsModalService, private spinner: NgxSpinnerService, private rolesService: RolesService) { }

  ngOnInit(): void {
    this.filterForm = this.fb.group({
      'Name': [],
      'Active': [true],
      'RoleIds': [],
    });

    this.filterForm.valueChanges
      .pipe(debounceTime(500))
      .subscribe(() => this.filter())

    this.filter();
  }

  public list() {
    this.spinner.show();
    this.usersService.list(this.usersRequest)
      .pipe(finalize(() => this.spinner.hide()))
      .subscribe(response => {
        this.response = response;
      })
  }

  public filter() {
    this.usersRequest = new UserListRequest({ ...this.filterForm.value, OrdenationField: 'Id', OrdenationType: OrdenationTypeEnum.Descendent });
    this.list()
  }

  public openUserRegisterModal(user?: UserResponse) {
    this.userRegisterModalRef = this.bsModalService.show(UserRegisterModalComponent, {
      class: 'modal-xl',
      initialState: { user: user }
    });

    this.userRegisterModalRef.content.saveEmitter.subscribe(() => {
      this.usersRequest = new UserListRequest({});
      this.resetForm();
    })
  }

  public changeStatus(user: UserResponse) {
    this.spinner.show();
    this.usersService.changeStatus(user.Id)
      .pipe(finalize(() => this.spinner.hide()))
      .subscribe(() => {
        this.list();
      })
  }

  public resetForm() {
    this.filterForm.reset({ Active: true });
    this.list();
  }
}
