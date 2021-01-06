package be.ugent.reeks1;

import static org.junit.jupiter.api.Assertions.assertEquals;

import org.junit.jupiter.api.Test;
import org.springframework.boot.test.autoconfigure.web.client.AutoConfigureWebClient;
import org.springframework.boot.test.autoconfigure.web.reactive.AutoConfigureWebTestClient;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.boot.test.context.SpringBootTest.WebEnvironment;
import org.springframework.http.MediaType;
import org.springframework.test.web.reactive.server.WebTestClient;
import org.springframework.beans.factory.annotation.Autowired;

@SpringBootTest(webEnvironment = WebEnvironment.RANDOM_PORT)
@AutoConfigureWebTestClient
class Reeks1ApplicationTests {

	@Autowired
	private WebTestClient webTestClient;

	@Test
	void contextLoads() {
	}

	@Test
	void getAllTest(){
		webTestClient.get().uri("/posts")
			.accept(MediaType.APPLICATION_JSON_UTF8)
			.exchange()
			.expectStatus().isOk()
			.expectHeader().contentType(MediaType.APPLICATION_JSON_UTF8)
			.expectBodyList(Blogpost.class);
	}

	@Test
	void addTest(){
		webTestClient.post().uri("/posts")
			.contentType(MediaType.APPLICATION_JSON_UTF8)
			.accept(MediaType.APPLICATION_JSON_UTF8)
			.bodyValue(new Blogpost("webtest title", "webtest content"))
			.exchange()
			.expectStatus().isCreated()
			.expectHeader().exists("location");
	}

	@Test
	void getSingleTest(){
		String locationUri = webTestClient.post().uri("/posts")
			.contentType(MediaType.APPLICATION_JSON_UTF8)
			.accept(MediaType.APPLICATION_JSON_UTF8)
			.bodyValue(new Blogpost("webtest title", "webtest content"))
			.exchange()
			.expectStatus().isCreated()
			.expectHeader().exists("location")
			.returnResult(Blogpost.class).getResponseHeaders().get("location").get(0);

		webTestClient.get().uri(locationUri)
			.accept(MediaType.APPLICATION_JSON_UTF8)
			.exchange()
			.expectStatus().isOk()
			.expectHeader().contentType(MediaType.APPLICATION_JSON_UTF8)
			.expectBody(Blogpost.class);

	}

	@Test
	void putTest(){
		String locationUri = webTestClient.post().uri("/posts")
			.contentType(MediaType.APPLICATION_JSON_UTF8)
			.accept(MediaType.APPLICATION_JSON_UTF8)
			.bodyValue(new Blogpost("webtest title", "webtest content"))
			.exchange()
			.expectStatus().isCreated()
			.expectHeader().exists("location")
			.returnResult(Blogpost.class).getResponseHeaders().get("location").get(0);

		Blogpost upTeDatenPost = webTestClient.get().uri(locationUri)
			.accept(MediaType.APPLICATION_JSON_UTF8)
			.exchange()
			.expectBody(Blogpost.class)
			.returnResult()
			.getResponseBody();

		String nieuweTitel = "upgedate title";
		String nieuweContent = "upgedate content";
		upTeDatenPost.setTitle(nieuweTitel);
		upTeDatenPost.setContent(nieuweContent);

		webTestClient.put().uri("/posts")
			.bodyValue(upTeDatenPost)
			.exchange()
			.expectStatus().isNoContent();

		Blogpost geupdatePost = webTestClient.get().uri(locationUri)
			.accept(MediaType.APPLICATION_JSON_UTF8)
			.exchange()
			.expectBody(Blogpost.class)
			.returnResult()
			.getResponseBody();

		assertEquals(geupdatePost.getTitle(), nieuweTitel);
		assertEquals(geupdatePost.getContent(), nieuweContent);
	}

	@Test
	void deleteTest(){
		String locationUri = webTestClient.post().uri("/posts")
			.contentType(MediaType.APPLICATION_JSON_UTF8)
			.accept(MediaType.APPLICATION_JSON_UTF8)
			.bodyValue(new Blogpost("webtest title", "webtest content"))
			.exchange()
			.expectStatus().isCreated()
			.expectHeader().exists("location")
			.returnResult(Blogpost.class).getResponseHeaders().get("location").get(0);

		webTestClient.delete().uri(locationUri)
			.exchange()
			.expectStatus().isNoContent();

		webTestClient.get().uri(locationUri)
			.exchange()
			.expectStatus().isNotFound();

	}

}
