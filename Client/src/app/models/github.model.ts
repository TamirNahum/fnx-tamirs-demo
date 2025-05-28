export interface GitHubRepositoryResponse {
    total_count: number;
    items: GitHubRepository[];
}

export interface GitHubRepository {
    bookmarked: boolean;
    id: number;
    name: string;
    full_name: string;
    description: string;
    html_url: string;
    owner: RepositoryOwner;
}

export interface RepositoryOwner {
    login: string;
    avatar_url: string;
}
