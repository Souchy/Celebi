package espeon.auth.jade;

public enum UserLevel {
	anonymous,
	normal,
	test,
	moderator,
	administrator,
	founder;

	public int id() {
		return ordinal();
	}
	public static final String name_anonymous = "anonymous";
	public static final String name_user = "normal";
	public static final String name_test = "test";
	public static final String name_moderator = "moderator";
	public static final String name_admin = "administrator";
	public static final String name_founder = "founder";
}
