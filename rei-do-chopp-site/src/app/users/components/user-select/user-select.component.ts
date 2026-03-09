import { CommonModule } from '@angular/common';
import { Component, EventEmitter, forwardRef, Injector, OnInit, Output } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { debounceTime, map, Subject } from 'rxjs';
import { UserListRequest } from '../../models/requests/user-list.request';
import { UserResponse } from '../../models/responses/user.response';
import { UsersService } from '../../services/users.service';

@Component({
  selector: 'app-users-select',
  imports: [CommonModule, NgSelectModule, FormsModule],
  templateUrl: './user-select.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => UsersSelectComponent),
      multi: true
    }
  ]
})
export class UsersSelectComponent implements ControlValueAccessor, OnInit {
  @Output('change') changeEmmiter = new EventEmitter<UserResponse>();

  value: number = null;
  onChange: any = () => { };
  onTouched: any = () => { };
  loading = false;

  search$ = new Subject<string>();

  request = new UserListRequest({Active: true});
  items: UserResponse[];

  constructor(private porductsService: UsersService, private injector: Injector) { }
  ngOnInit(): void {
    this.search$.pipe(debounceTime(400))
      .subscribe(term => this.list(term));
    setTimeout(() => { this.list(null, this.value); })
  }

  public list(term?: string, id?: number) {
    this.loading = true;
    this.request = new UserListRequest({ Id: id, Name: term, Active: true });
    this.porductsService.list(this.request)
    .subscribe(response => {
      this.loading = false;
      this.items = response.Data;
    })
  }

  writeValue(obj: any): void {
    this.value = obj;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  };

  emmitChange(user: UserResponse) {
    this.changeEmmiter.emit(user);
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState?(isDisabled: boolean): void {

  }

  updateValue(val: number) {
    this.value = val;
    if (!val) {
      this.list();
    }
    this.onChange(val);
    this.onTouched();

  }
}
