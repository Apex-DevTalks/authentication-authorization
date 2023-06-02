import { useQuery, UseQueryOptions } from '@tanstack/react-query';
import { useFetchData } from './graphql-fetcher';
export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  DateTime: any;
};

export type CourseTypeDto = {
  __typename?: 'CourseTypeDTO';
  courseName: Scalars['String'];
  description: Scalars['String'];
  id: Scalars['Int'];
  imageURL: Scalars['String'];
  lastEditionDate: Scalars['DateTime'];
  professorName: Scalars['String'];
};

export type MyCustomTypeDto = {
  __typename?: 'MyCustomTypeDTO';
  dateCreated: Scalars['DateTime'];
  id: Scalars['Int'];
};

export type Query = {
  __typename?: 'Query';
  accounts_getAllCourses?: Maybe<Array<Maybe<CourseTypeDto>>>;
  accounts_getCourseById?: Maybe<CourseTypeDto>;
  accounts_getDataAnonymously?: Maybe<MyCustomTypeDto>;
  authorization_addRole?: Maybe<Scalars['Boolean']>;
  authorization_addRoleToUser?: Maybe<Scalars['Boolean']>;
  authorization_createUser?: Maybe<Scalars['Boolean']>;
  authorization_getDataAsAdmin?: Maybe<Scalars['String']>;
  authorization_getDataAsVisitor?: Maybe<MyCustomTypeDto>;
  authorization_getRoles?: Maybe<Array<Maybe<Scalars['String']>>>;
  authorization_login?: Maybe<Scalars['String']>;
};


export type QueryAccounts_GetCourseByIdArgs = {
  id?: InputMaybe<Scalars['Int']>;
};


export type QueryAuthorization_AddRoleArgs = {
  name?: InputMaybe<Scalars['String']>;
};


export type QueryAuthorization_AddRoleToUserArgs = {
  id?: InputMaybe<Scalars['String']>;
  role?: InputMaybe<Scalars['String']>;
};


export type QueryAuthorization_CreateUserArgs = {
  email?: InputMaybe<Scalars['String']>;
  password?: InputMaybe<Scalars['String']>;
  role?: InputMaybe<Scalars['String']>;
  userName?: InputMaybe<Scalars['String']>;
};


export type QueryAuthorization_LoginArgs = {
  email: Scalars['String'];
  password: Scalars['String'];
};

export type Courses_DataQueryVariables = Exact<{ [key: string]: never; }>;


export type Courses_DataQuery = { __typename?: 'Query', accounts_getAllCourses?: Array<{ __typename?: 'CourseTypeDTO', id: number, courseName: string, professorName: string, description: string, imageURL: string, lastEditionDate: any } | null> | null };


export const Courses_DataDocument = `
    query courses_data {
  accounts_getAllCourses {
    id
    courseName
    professorName
    description
    imageURL
    lastEditionDate
  }
}
    `;
export const useCourses_DataQuery = <
      TData = Courses_DataQuery,
      TError = unknown
    >(
      variables?: Courses_DataQueryVariables,
      options?: UseQueryOptions<Courses_DataQuery, TError, TData>
    ) =>
    useQuery<Courses_DataQuery, TError, TData>(
      variables === undefined ? ['courses_data'] : ['courses_data', variables],
      useFetchData<Courses_DataQuery, Courses_DataQueryVariables>(Courses_DataDocument).bind(null, variables),
      options
    );