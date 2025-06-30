import { TestBed } from '@angular/core/testing';
import {  provideStore,  Store } from '@ngxs/store';
import { CurrentUserState, CurrentUserStateModel } from './current-user.state';
import { SetCurrentUserAction } from './current-user.actions';
import { UserModel } from '../models/user.model';

describe('CurrentUser store', () => {
  let store: Store;
  beforeEach(() => {
    TestBed.configureTestingModule({
       providers: [provideStore([CurrentUserState])]
      
    });

    store = TestBed.inject(Store);
  });

  it('should create an action and set an item', () => {
    const expected: CurrentUserStateModel = {
      user : new UserModel()
    };
    store.dispatch(new SetCurrentUserAction(new UserModel()));
    const actual = store.selectSnapshot(CurrentUserState.getUser);
    expect(actual).toEqual(new UserModel());
  });

});
